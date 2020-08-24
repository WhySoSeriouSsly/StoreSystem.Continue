using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using StoreSystem.DataAcccesLayer.Abstract;

namespace StoreSystem.Business.Abstract
{
    public interface IFileService
    {
        byte[] ExcelFileGet();
        string PdfFileGet();
    }
}
