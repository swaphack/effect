﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.Schedulers
{
    public interface IScheduler
    {

        void OnTimeUp(float dt);
    }
}