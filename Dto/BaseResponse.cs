namespace Artifactan.Dto;

public record BaseResponse<T>(string Message, T Data);