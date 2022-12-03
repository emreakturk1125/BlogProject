﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA.BlogProject.Entities.ComplexTypes
{
    public enum OrderBy
    {
        [Display(Name = "Tarih")] 
        Date = 0,
        [Display(Name = "Okunma Sayısı")] 
        ViewCount = 1,
        [Display(Name = "Yorum Sayısı")] 
        CommentCount = 2
    }
}
