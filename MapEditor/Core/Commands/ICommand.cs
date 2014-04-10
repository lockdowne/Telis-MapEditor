using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditor.Models;

namespace MapEditor.Core.Commands
{
    /// <summary>
    /// Base for all commands
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
