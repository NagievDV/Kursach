//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Goncharov_Kursach
{
    using System;
    using System.Collections.Generic;
    
    public partial class Services
    {
        public int id { get; set; }
        public int type { get; set; }
        public int responsible { get; set; }
        public int client { get; set; }
    
        public virtual Client Client1 { get; set; }
        public virtual Service_type Service_type { get; set; }
        public virtual Staff Staff { get; set; }
    }
}