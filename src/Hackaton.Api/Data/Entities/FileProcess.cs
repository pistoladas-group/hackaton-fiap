using Hackaton.Shared.Models;

namespace Hackaton.Api.Data.Entities;

public class FileProcess : Entity
{
    public Guid FileId { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }
    public ProcessTypeEnum ProcessTypeId { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

    //EF
    protected FileProcess()
    {
    }

    public FileProcess(Guid fileId, ProcessStatusEnum processStatusId, ProcessTypeEnum processTypeId, string? errorMessage, DateTime? startedAt, DateTime? finishedAt)
    {
        FileId = fileId;
        ProcessStatusId = processStatusId;
        ProcessTypeId = processTypeId;
        ErrorMessage = errorMessage;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
    }
}
