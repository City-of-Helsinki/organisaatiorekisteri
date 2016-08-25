using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationRegister.Application.Organization
{
    public interface ILocalizedText
    {

        string GetDescription(string languageCode);
        string GetHomepageUrl(string languageCode);
        string GetName(string languageCode);
    }
}
