using EA.BlogProject.Entities.Dtos;
using EA.BlogProject.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Services.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto);  
        IResult SendContactEmail(EmailSendDto emailSendDto);  
    }
}
