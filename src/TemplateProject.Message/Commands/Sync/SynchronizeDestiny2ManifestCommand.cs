using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Sync;

public class SynchronizeDestiny2ManifestCommand : ICommand
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one language is required.")]
    public List<string> Languages { get; set; } = ["en", "zh-cht", "zh-chs"];
    
    public List<string>? Components { get; set; }
    
    public bool ForceSync { get; set; }
}

public class SynchronizeDestiny2ManifestResponse : TemplateProjectResponse
{
}