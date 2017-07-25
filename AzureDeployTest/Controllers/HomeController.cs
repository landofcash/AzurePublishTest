using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzureDeployTest.Controllers
{

    public class SelectOption
    {
        //[PersistentProperty("Text")]
        public string Text { get; set; }
        //[PersistentProperty("Value")]
        public string Value { get; set; }
    }
    public class AgentModel
    {
        //Agent Information

        /// <summary>
        /// First Name
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// Middle Name
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string LName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Case manager ID
        /// </summary>
        public int CaseManagerId { get; set; }

        /// <summary>
        /// Case Manager for dropdown
        /// </summary>
        public List<SelectOption> CaseManagerOptions { get; set; }

        /// <summary>
        /// Sales Executive ID
        /// </summary>
        public int SalesExecutiveId { get; set; }

        /// <summary>
        /// Sales Executives for dropdown
        /// </summary>
        public List<SelectOption> SalesExecutiveOptions { get; set; }

        //Contact Information
        public string WorkAddress1 { get; set; }
        public string WorkAddress2 { get; set; }
        public string WorkCity { get; set; }
        public string WorkState { get; set; }
        public string WorkZip { get; set; }
        public string WorkPhone { get; set; }
        public string WorkFax { get; set; }
        public string Pager { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }

        //Paperwork Handling Information
        public string HandlingInstruction { get; set; }
        public string MailingAttn { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Fax { get; set; }
        public int AgencyId { get; set; }

        //Insurance Company Information
        public int InsuranceCompanyId { get; set; }
        public List<SelectOption> InsuranceCompanyOptions { get; set; }

        public int OriginId { get; set; }
        public List<SelectOption> OriginOptions { get; set; }

        public string SpecialNotes { get; set; }
        public string AgencyCode { get; set; }
        public string AgentCode { get; set; }
        public string RewardScheme { get; set; }
        public List<SelectOption> RewardSchemeOptions { get; set; }

        public bool CanViewExam { get; set; }
        public bool ReceiveEmailUpdates { get; set; }
        public bool AutoEmailExam { get; set; }
        public bool BirthDayReminder { get; set; }
        public bool EAnnualReport { get; set; }
    }


    public class HomeController : Controller
    {

        public static List<SelectOption> GetCaseManagerOptions()
        {
            return new List<SelectOption>()
            {
                new SelectOption {Text="Manager Option 1", Value = "1" },
                new SelectOption {Text="Manager Option 2", Value = "2" },
            };
        }

        public static List<SelectOption> GetSalesExecutiveOptions()
        {
            return new List<SelectOption>()
            {
                new SelectOption {Text="SalesExecutive Option 1", Value = "1" },
                new SelectOption {Text="SalesExecutive Option 2", Value = "2" },
            };
        }

        public static List<SelectOption> GetInsuranceCompanyOptions()
        {
            return new List<SelectOption>()
            {
                new SelectOption {Text="Insurance Company 1", Value = "1" },
                new SelectOption {Text="Insurance Company 2", Value = "2" },
            };
        }
        public static List<SelectOption> GetOriginOptions()
        {
            return new List<SelectOption>()
            {
                new SelectOption {Text="Origin 1", Value = "1" },
                new SelectOption {Text="Origin 2", Value = "2" },
            };
        }
        public static List<SelectOption> GetRewardSchemeOptions()
        {
            return new List<SelectOption>()
            {
                new SelectOption {Text="Reward Scheme 1", Value = "1" },
                new SelectOption {Text="Reward Scheme 2", Value = "2" },
            };
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Vuejs()
        {
            var model = new AgentModel();
            model.CaseManagerOptions = GetCaseManagerOptions();
            model.SalesExecutiveOptions = GetSalesExecutiveOptions();
            model.InsuranceCompanyOptions = GetInsuranceCompanyOptions();
            model.OriginOptions = GetOriginOptions();
            model.RewardSchemeOptions = GetRewardSchemeOptions();
            return View(model);
        }

        public JsonResult AddPost(AgentModel model)
        {
            Console.WriteLine(model);
            return Json(new { model, result = true });
        }
    }
}