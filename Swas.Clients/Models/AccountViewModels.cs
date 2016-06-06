using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Swas.Clients.Models
{
    public class LoginViewModel
    {
        #region Properties

        [Display(Name = "მომხმარებელი:"),
        StringLength(255),
        Required(ErrorMessage = "მიუთითეთ მომხმარებლის სახელი!")]
        public string UserName { get; set; }

        [Display(Name = "პაროლი:"),
        DataType(DataType.Password),
        Required(ErrorMessage = "მიუთითეთ პაროლი!")]
        public string Password { get; set; }

        #endregion
    }

}
