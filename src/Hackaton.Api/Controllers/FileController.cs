using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hackaton.Api.Data;
using Hackaton.Api.Models;

namespace TechBox.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private ApplicationDbContext _context { get; set; }

    public FilesController(ApplicationDbContext context)
    {
        _context = context;
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
        //TODO: Salvar local
        //TODO: Salvar no banco para controle (tabela FileProcesses)
        //TODO: Postar na fila

        //TODO: Rollback de tudo se der algo errado



        // .....

        //var result = _remoteFileStorageService.ValidateFile(formFile);

        //if (!result.IsSuccess)
        //{
        //    return BadRequest(new ApiResponse(result.Errors));
        //}

        //var existingFileWithSameName = await _fileRepository.CheckIfFileExistsByFileNameAsync(formFile.FileName);

        //if (existingFileWithSameName)
        //{
        //    var enumeratedExistingFiles = await _fileRepository.ListFilesByFileName(formFile.FileName);
        //    var existingFiles = enumeratedExistingFiles.ToList();
        //    foreach (var file in existingFiles)
        //    {
        //        await _fileRepository.DeleteFileByIdAsync(file.Id);
        //    }
        //}

        //var fileToAdd = new AddFileDto(formFile.FileName, formFile.Length, formFile.ContentType);
        //var id = await _fileRepository.AddFileAsync(fileToAdd);

        //var existingUploadFileLog = await _fileRepository.CheckFileLogByFileIdAndProcessTypeIdAsync(id, ProcessTypesEnum.Upload);

        //if (!existingUploadFileLog)
        //{
        //    await _fileRepository.AddFileLogAsync(new AddFileLogDto(id, ProcessTypesEnum.Upload));
        //}

        //_localFileStorageService.SaveFile(formFile, id);

        return CreatedAtAction(nameof(GetFileById), null, new ApiResponse(data: null));
    }

    /// <summary>
    /// Delete a file by id
    /// </summary>
    /// <response code="202">Operation accepted and being processed</response>
    /// <response code="404">The resource was not found</response>
    /// <response code="500">An internal error occurred</response>
    [HttpDelete("{id:guid}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid id)
    {
        //TODO: Salvar local
        //TODO: Salvar no banco para controle (tabela FileProcesses)
        //TODO: Postar na fila

        //TODO: Rollback de tudo se der algo errado


        //var file = await _fileRepository.GetFileByIdAsync(id);

        //if (file is null)
        //{
        //    return NotFound(new ApiResponse(error: "no file found"));
        //}

        //await _fileRepository.UpdateFileProcessStatusByIdAsync(id, ProcessStatusEnum.Pending);

        //var existingFileLog = await _fileRepository.CheckFileLogByFileIdAndProcessTypeIdAsync(id, ProcessTypesEnum.Delete);

        //if (!existingFileLog)
        //{
        //    await _fileRepository.AddFileLogAsync(new AddFileLogDto(id, ProcessTypesEnum.Delete));
        //}

        //await _fileRepository.DeleteFileByIdAsync(id);

        return Accepted(new ApiResponse());
    }
}
