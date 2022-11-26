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
        IResult Send(EmailSendDto emailSendDto); // alper@altu.dev
        IResult SendContactEmail(EmailSendDto emailSendDto); // alper@altu.dev info@programmersblog.com iletisim@programmersblog.com
    }
}
