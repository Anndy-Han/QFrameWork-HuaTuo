using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFrameWork
{
    public interface IProcedureManager
    {
        void ChangeProcedure(Msg msg);

        void LoadProceduces(List<Msg> msgs);

        Msg CreateProcedureEnter(string str);

        Msg CreateProcedureLeave(string str);
    }
}
