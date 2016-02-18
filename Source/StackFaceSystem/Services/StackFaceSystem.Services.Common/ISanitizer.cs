namespace StackFaceSystem.Services.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISanitizer
    {
        string Sanitize(string html);
    }
}