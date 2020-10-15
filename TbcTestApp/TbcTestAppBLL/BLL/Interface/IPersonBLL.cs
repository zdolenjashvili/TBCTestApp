using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Models.FilterModels;
using TbcTestAppCore.Models;

namespace TbcTestAppBLL.BLL.Interface
{
    public interface IPersonBLL
    {
        Task<Result> AddPerson(PersonModel personModel);
        Task<Result> UpdatePerson(PersonUpdateModel personModel);
        Task<Result> DeletePerson(int personId);
        Task<Result> AddRelationPerson(PersonModel personModel);
        Task<Result> DeleteRelationPerson(int personId);
        Task<GenericResult<PersonModel>> GetPerson(int personId);
        Task<Result> UploadUpdatePersonFile(int? personId, IFormFileCollection filesData);
        Task<GenericResult<List<PersonReportModel>>> GetPersonRelationReport();
        GenericResult<Page<PersonModel>> GetFilteredPerosns(int pageIndex, int pageSize, PersonFilterModel personFilsterModel);
        Task<GenericResult<FileModel>> DownloadPersonFile(int? personId);
    }
}
