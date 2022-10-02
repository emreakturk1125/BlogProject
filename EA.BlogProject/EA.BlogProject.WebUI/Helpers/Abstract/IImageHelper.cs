using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EA.BlogProject.Entities.Dtos;
using EA.BlogProject.Shared.Utilities.Results.Abstract;
using EA.BlogProject.Entities.ComplexTypes;

namespace EA.BlogProject.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile pictureFile,PictureType pictureType, string folderName = null);
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
