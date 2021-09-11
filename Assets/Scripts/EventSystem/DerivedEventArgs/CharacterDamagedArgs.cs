using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterDamagedArgs : EventArgs
{
    public CharacterDamagedArgs(Character character)
    {
        this.character = character;
    }

    public Character character { get; }
}