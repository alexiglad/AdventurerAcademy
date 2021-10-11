using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterDeathEventArgs : EventArgs
{
    public CharacterDeathEventArgs(Character character)
    {
        this.character = character;
    }

    public Character character { get; }
}