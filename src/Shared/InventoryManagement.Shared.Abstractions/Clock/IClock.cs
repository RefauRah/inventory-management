﻿namespace InventoryManagement.Shared.Abstractions.Clock;

public interface IClock
{
    DateTime CurrentDate();

    DateTime CurrentServerDate();
}