using Microsoft.AspNetCore.Mvc;
using NetCore5Test.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore5Test.Controllers
{
    public class InputTESTController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var mdl = new VMModel() { name = "aa\"aa" };
            var str = await this.RenderViewAsync("Info_Partial_View", mdl, true);
            return View(mdl);
        }
        [HttpPost]
        public async Task<IActionResult> Index(VMModel model)
        {
            var files = Request.Form.Files;
            //model.file.CopyTo(target stream);
            using (FileStream fs = new FileStream("d:\\UploadTest.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                model.file.CopyTo(fs);
                fs.Flush();
            }

            #region un-tested code
            //var multipartFormDataContent = new System.Net.Http.MultipartFormDataContent();
            //multipartFormDataContent.Add(new System.Net.Http.StringContent("name value"), "name");
            //multipartFormDataContent.Add(new System.Net.Http.StringContent("22"), "Age");
            //multipartFormDataContent.Add(new System.Net.Http.StreamContent(new MemoryStream()), "fileName");
            //var response = await new System.Net.Http.HttpClient().PostAsync("url", multipartFormDataContent);
            #endregion

            return View(model);
        }

        [HttpPost]
        public IActionResult AjaxUpdate(VMModel model)
        {
            //var dt = model.CreateDate.Value.ToLocalTime();
            var files = Request.Form.Files;
            ////model.file.CopyTo(target stream);
            //using (FileStream fs = new FileStream("d:\\UploadTest.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    model.file.CopyTo(fs);
            //    fs.Flush();
            //}

            MemoryStream ms = new MemoryStream();
            model.fCollection[0].CopyTo(ms);
            ms.Flush();
            var bytes = ms.ToArray();
            ms.Close();
            var txt = Encoding.UTF8.GetString(bytes);
            return PartialView("Info_Partial_View", model);
        }
        [HttpPost]
        public IActionResult AjaxUpdate_Spec([FromBody]VMModel model)
        {
            var dt = model.CreateDate.Value.ToLocalTime();
            //var files = Request.Form.Files;
            ////model.file.CopyTo(target stream);
            //using (FileStream fs = new FileStream("d:\\UploadTest.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    model.file.CopyTo(fs);
            //    fs.Flush();
            //}
            return PartialView("Info_Partial_View", model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AjaxUpdate_ViewJsonAsync([FromBody]VMModel model)
        {
            var dt = model.CreateDate.Value.ToLocalTime();
            return Json(new { ResultView = await this.RenderViewAsync("Info_Partial_View", model, true) });
        }
    }

    public class VMModel
    {
        [Required]
        [RegularExpression("^[\\da-z]{1,5}", ErrorMessage = "Name format Error!")]
        public string name { get; set; }
        public string sname { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile file { get; set; }
        public Microsoft.AspNetCore.Http.IFormFileCollection fCollection { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Age { get; set; }
    }
}
