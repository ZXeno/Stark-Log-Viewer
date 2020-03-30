namespace Stark
{
    using System.ComponentModel.DataAnnotations;

    public enum LogTypeEnum
    {
        [Display(Name = "--Select Log--")]
        SelectOne = 0,

        [Display(Name = "Application")]
        Application = 1,

        [Display(Name = "Security")]
        Security = 2,

        [Display(Name = "Setup")]
        Setup = 3,

        [Display(Name = "System")]
        System = 4
    }
}
