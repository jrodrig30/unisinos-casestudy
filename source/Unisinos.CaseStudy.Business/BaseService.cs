using System;
using System.Collections.Generic;
using System.Text;
using Unisinos.CaseStudy.Data;

namespace Unisinos.CaseStudy.Business
{
    public abstract class BaseService
    {
        public BemPromotoraContext Context { get; set; }

        public BaseService(BemPromotoraContext context)
        {
            this.Context = context;
        }
    }
}
