using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace Api.Controllers;

[ApiController]
[Route("image")]
public class ImageController : ControllerBase
{
    private static readonly string[] ValidTypes = ["image/jpeg", "image/png"];
    private const int MaxImageHeight = 480;
    private const int MaxImageWidth = 320;

    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{imageId:guid}")]
    public async Task<IActionResult> GetImage(Guid imageId)
    {
        var image = await _imageService.GetImageByIdAsync(imageId);
        if (image is null)
            return NotFound($"Image with id {imageId} does not exist.");

        return File(image.Data, image.Type, image.Name);
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile? file, [FromQuery] Guid? personId,
        [FromQuery] Guid? filmId, [FromQuery] Guid? showId, [FromQuery] Guid? episodeId)
    {
        // Count how many query parameters are not null
        var nonNullCount = 0;
        if (personId.HasValue) nonNullCount++;
        if (filmId.HasValue) nonNullCount++;
        if (showId.HasValue) nonNullCount++;
        if (episodeId.HasValue) nonNullCount++;

        // Check that exactly one parameter is not null
        if (nonNullCount != 1)
            return BadRequest("Exactly one query parameter (personId, filmId, showId, episodeId) must be provided.");
        
        // Validate file length
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");
        
        // check if it is an image
        if (!ValidTypes.Contains(file.ContentType))
            return BadRequest("Invalid file type. Only JPEG and PNG are allowed.");
        
        // Check the resolution
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        using var image = await Image.LoadAsync(memoryStream);
        if (image.Width > MaxImageWidth || image.Height > MaxImageHeight)
            return BadRequest($"Image resolution exceeds the maximum allowed dimensions of {MaxImageWidth}x{MaxImageHeight}.");
        
        var imageCreate = new ImageCreate(memoryStream.ToArray(), file.FileName, file.ContentType, personId, filmId,
            showId, episodeId);
        
        var result = await _imageService.CreateImageAsync(imageCreate);

        return result is not null ? Created($"image/{result}", result) : Problem("Failed to save image.");
    }
}