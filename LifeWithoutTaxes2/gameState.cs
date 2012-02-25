using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace LifeWithoutTaxes2
{
    public enum stateGame
    {
        mainMenu, 
        credits,
        gameIntro,
        game1Intro,
        game1,
        game1Win,
        game1Fail,
        game2Intro,
        game2,
        game2Win,
        game2Fail,
        game3,
        game3Win,
        game3Fail,
        game4Intro,
        game4,
        game4Fail,
        gameOutro
    };
    
}
