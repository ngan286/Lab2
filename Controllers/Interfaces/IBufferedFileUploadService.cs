namespace Lab_02.Controllers.Interfaces;

public interface IBufferedFileUploadService
{
    Task<bool> UploadFile(IFormFile file);
}