using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hackaton.Api.Data;
using Hackaton.Api.Models;
using Hackaton.Api.Services;
using Hackaton.Api.Data.Entities;
using System.Linq;

namespace TechBox.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private ApplicationDbContext _context { get; set; }
    private readonly IRemoteFileStorageService _remoteFileStorageService;
    private readonly ILocalFileStorageService _localFileStorageService;

    public FilesController(ApplicationDbContext context, IRemoteFileStorageService remoteFileStorageService, ILocalFileStorageService localFileStorageService)
    {
        _context = context;
        _remoteFileStorageService = remoteFileStorageService;
        _localFileStorageService = localFileStorageService;
    }

    /// <summary>
    /// Get all files
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="500">An internal error occurred</response>
    [HttpGet("")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllFiles([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return BadRequest(new ApiResponse(error: "page number and page size must be greater than 0."));
        }

        var storedFiles = await _context.Files.AsNoTracking().Skip(pageNumber).Take(pageSize).ToListAsync();

        return Ok(new ApiResponse(data: null)); // TODO: Retornar os zips
    }

    /// <summary>
    /// Get a file by id
    /// </summary>
    /// <response code="200">Returns the resource data</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="404">The resource was not found</response>
    /// <response code="500">An internal error occurred</response>
    [HttpGet("{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetFileById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new ApiResponse(error: "Invalid fileId"));
        }

        var storedFile = await _context.Files.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (storedFile is null)
        {
            return NotFound(new ApiResponse(error: "File not found"));
        }

        return Ok(new ApiResponse(data: storedFile));
    }

    /// <summary>
    /// Upload a file
    /// </summary>
    /// <response code="201">Returns the created resource endpoint in response header</response>
    /// <response code="400">There is a problem with the request</response>
    /// <response code="500">An internal error occurred</response>
    [HttpPost("")]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UploadFile(IFormFile formFile)
    {
        var result = _remoteFileStorageService.ValidateFile(formFile);

        if (!result.IsSuccess)
        {
            return BadRequest(new ApiResponse(result.Errors));
        }

        var existingFileWithSameName = await _context.Files.AsNoTracking().Where(x => x.Name == formFile.FileName).ToListAsync();

        if (existingFileWithSameName.Count > 0)
        {
            foreach (var fileToDelete in existingFileWithSameName)
            {
                fileToDelete.IsDeleted = true;
            }
        }

        var fileToAdd = new Hackaton.Api.Data.Entities.File(formFile.FileName, formFile.Length, null, formFile.ContentType, ProcessStatusEnum.Pending);
        _context.Files.Add(fileToAdd);

        var existingUploadFileLog = await _context.FileProcesses.AsNoTracking().FirstOrDefaultAsync(x => x.FileId == fileToAdd.Id && x.ProcessTypeId == ProcessTypeEnum.Upload);

        if (existingUploadFileLog == null)
        {
            _context.FileProcesses.Add(new FileProcess(fileToAdd.Id, ProcessTypeEnum.Upload));
        }

        // Salva local em caso de dar erro na hora de subir para o Azure
        _localFileStorageService.SaveFile(formFile, fileToAdd.Id);

        // Faz upload para o Azure para o Consumer conseguir baixar o arquivo e processar
        var file = _localFileStorageService.GetFileById(fileToAdd.Id, formFile.FileName);
        var uploadedFileUri = await _remoteFileStorageService.UploadFileAsync(file, formFile.FileName, formFile.ContentType);

        await _context.SaveChangesAsync();

        //TODO: Postar na fila

        return CreatedAtAction(nameof(GetFileById), null, new ApiResponse(data: null));
    }
}
