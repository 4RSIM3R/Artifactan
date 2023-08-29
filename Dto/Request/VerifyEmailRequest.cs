using System.ComponentModel.DataAnnotations;

namespace Artifactan.Dto.Request;

public class VerifyEmailRequest
{
    public string Email { get; set; } = string.Empty;
    
    public string Otp { get; set; } = string.Empty;
}