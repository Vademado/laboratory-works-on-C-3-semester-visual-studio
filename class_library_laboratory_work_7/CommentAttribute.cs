using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library_laboratory_work_7
{
    [AttributeUsage(AttributeTargets.Class)]
    class CommentAttribute : Attribute
    {
        public string Comment { get; }

        public CommentAttribute()
        {
            Comment = "No comment";
        }
        public CommentAttribute(string comment)
        {
            Comment = comment;
        }
    }
}
