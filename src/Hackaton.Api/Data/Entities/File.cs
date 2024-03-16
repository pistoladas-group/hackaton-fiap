using Hackaton.Shared.Models;

namespace Hackaton.Api.Data.Entities;

public class File : Entity
{
    public string Name { get; set; }
    public long SizeInBytes { get; set; }
    public string? Url { get; set; }
    public string ContentType { get; set; }
    public ProcessStatusEnum ProcessStatusId { get; set; }

    //EF
    protected File()
    {
    }

    public File(string name, long sizeInBytes, string? url, string contentType, ProcessStatusEnum processStatusId)
    {
        Name = name;
        SizeInBytes = sizeInBytes;
        Url = url;
        ContentType = contentType;
        ProcessStatusId = processStatusId;
    }
}