﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public interface ICityBikeDataFetcher
{
    Task<int> GetBikeCountInStation(string stationName);
}

