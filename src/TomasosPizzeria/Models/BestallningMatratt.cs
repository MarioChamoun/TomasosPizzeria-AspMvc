﻿using System;
using System.Collections.Generic;

namespace TomasosPizzeria.Models
{
    public partial class BestallningMatratt
    {
        public int MatrattId { get; set; }
        public int BestallningId { get; set; }

        public virtual Bestallning Bestallning { get; set; }
        public virtual Matratt Matratt { get; set; }
    }
}
