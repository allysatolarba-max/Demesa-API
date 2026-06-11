using Microsoft.AspNetCore.Mvc;
using OnlineBankingAppService;
using OnlineBankingModels;
using OnlineBanksAPI.Models;

namespace OnlineBanksAPI.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class BankingController : ControllerBase
    {
        private readonly BankingBusiness _appService;

        public BankingController()
        {
            _appService = new BankingBusiness();
        }

        // GET ALL LOANS
        [HttpGet]
        public ActionResult<IEnumerable<BankingModel>> GetAllLoans()
        {
            var loans = _appService.GetLoans();
            return Ok(loans);
        }

        // GET BY ID
        [HttpGet("{id:guid}")]
        public ActionResult<BankingModel> GetLoanById(Guid id)
        {
            var loan = _appService.ReceiptLoans(id);

            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        // POST (CREATE LOAN)
        [HttpPost]
        public IActionResult CreateLoan([FromBody] LoanViewModel model)
        {
            if (model == null)
                return BadRequest("Loan data is required.");

            var result = _appService.CreateLoan(model.LoanAmount, model.LoanPeriod);

            if (result == null)
                return BadRequest("Invalid loan amount or period.");

            return CreatedAtAction(
                nameof(GetLoanById),
                new { id = result.LoanId },
                result
            );
        }

        // PATCH (UPDATE LOAN PERIOD ONLY - like your AppService EditLoans)
        [HttpPatch("{id:guid}")]
        public IActionResult UpdateLoan(Guid id, [FromBody] LoanViewModel model)
        {
            if (model == null)
                return BadRequest("Loan data is required.");

            var existing = _appService.ReceiptLoans(id);

            if (existing == null)
                return NotFound();

            _appService.EditLoans(id, model.LoanPeriod);

            return NoContent();
        }

        // DELETE LOAN
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteLoan(Guid id)
        {
            var existing = _appService.ReceiptLoans(id);

            if (existing == null)
                return NotFound();

            _appService.DeleteLoans(id);

            return NoContent();
        }
    }
}