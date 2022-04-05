using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QFrameWork
{
    public class UIType
    {
        public string path;

        public string name;

        public UIType(string path)
        {
            this.path = path;
            this.name = path.Substring(path.LastIndexOf('/')+1);
        }

        public override string ToString()
        {
            return string.Format("path:{0},name:{1}",path,name);
        }
    }
}
