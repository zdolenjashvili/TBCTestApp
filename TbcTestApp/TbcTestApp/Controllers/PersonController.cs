using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TbcTestAppBLL.BLL.Interface;
using TbcTestAppBLL.Models;
using TbcTestAppBLL.Models.FilterModels;
using TbcTestAppCore.Models;

namespace TbcTestAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBLL personBLL;

        public PersonController(IPersonBLL personBLL)
        {
            this.personBLL = personBLL;
        }


        /// <summary>
        /// პირვონების მონაცემების წამოღება
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GenericResult<PersonModel>> Get(int id)
        {
            return await personBLL.GetPerson(id);
        }


        /// <summary>
        /// პიროვნების ჩაწერა ბაზაში
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        [HttpPost("AddPerson")]
        public async Task<Result> AddPerson([FromBody] PersonModel personModel)
        {
            return await personBLL.AddPerson(personModel);
        }

        // პიროვნების მონაცემების განახლება
        [HttpPut("UpdatePerson")]
        public async Task<Result> UpdatePerson([FromBody] PersonUpdateModel personModel)
        {
            return await personBLL.UpdatePerson(personModel);
        }


        /// <summary>
        /// პიროვნების წაშლა
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<Result> Delete(int id)
        {
            return await personBLL.DeletePerson(id);
        }



        /// <summary>
        /// ფიზიკური პირის დაკავშირებული პიროვნების ჩაწერა ბაზაში
        /// </summary>
        /// <param name="personModel"></param>
        /// <returns></returns>
        [HttpPost("AddRelationPerson")]
        public async Task<Result> AddRelationPerson([FromBody] PersonModel personModel)
        {
            return await personBLL.AddRelationPerson(personModel);
        }



        /// <summary>
        /// დაკავშირებული პიროვნების წაშლა
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRelationPerson/{id}")]
        public async Task<Result> DeleteRelationPerson(int id)
        {
            return await personBLL.DeleteRelationPerson(id);
        }


        /// <summary>
        /// პიროვნების ფაილის დამატება/რედაქტირება
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="filesData"></param>
        /// <returns></returns>
        [HttpPost("UploadUpdatePersonImg/{personId}")]
        public async Task<Result> UploadUpdatePersonImg(int? personId)
        {

            try
            {
                var personFile = HttpContext.Request?.Form?.Files;
                return await personBLL.UploadUpdatePersonFile(personId, personFile);

            }
            catch (Exception exception)
            {
                return new Result(false, -3, $"ფაილის ატვირთვა/რედაქტირებისას მოხდა შეცდომა,{exception}");
            }

        }



        /// <summary>
        /// პიროვნების ფაილის ჩამოტვირთვა
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="parentPesronalId"></param>
        /// <returns></returns>
        [HttpGet("DownloadFiles/{personId}")]
        public async Task<IActionResult> DownloadFiles(int personId)
        {
            var result = await personBLL.DownloadPersonFile(personId);

            if (!result.IsSuccess)
            {
                return Content(result.Message);
            }

            var personFiles = result.Result;

            return File(personFiles.fileData, personFiles.fileType, personFiles.fileName);
        }


        /// <summary>
        ///  რეპორტი თუ რამდენი დაკავშირებული პირი ჰყავს თითოეულ ფიზიკურ პირს, კავშირის ტიპის მიხედვით
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetPersonRelationReport")]
        public async Task<GenericResult<List<PersonReportModel>>> GetPersonRelationReport()
        {
            return await personBLL.GetPersonRelationReport();
        }


        /// <summary>
        ///  ფიზიკური პირების სიის მიღება, სწრაფი ძებნის (სახელი, გვარი, პირადი ნომრის მიხედვით (დამთხვევა sql like პრინციპით)),
        ///  დეტალური ძებნის (ყველა ველის მიხედვით) და paging-ის ფუნქციით
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet("GetFilteredPerosns/{pageIndex:int}")]
        public GenericResult<Page<PersonModel>> GetFilteredPerosns(int pageIndex, PersonFilterModel personFilsterModel,[FromQuery] int pageSize = 10)
        {
            return personBLL.GetFilteredPerosns(pageIndex, pageSize, personFilsterModel);
        }

    }
}
