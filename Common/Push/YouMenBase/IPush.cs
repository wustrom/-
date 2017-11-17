using Common.Push.YouMenResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Push.YouMenBase
{
    public interface IPush
    {
        ReturnJsonClass SendMessage(PostUMengJsonBase paramsJsonObj);

        void AsynSendMessage(PostUMengJsonBase paramsJsonObj, Action<ReturnJsonClass> callback);

    }
}
