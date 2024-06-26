﻿using InventoryManagement.WebApi.Scopes;

namespace InventoryManagement.WebApi.Endpoints.RoleManagement.Scopes;

public class RoleManagementScopeReadOnly : IScope
{
    public string ScopeName => $"{nameof(RoleManagementScope)}.readonly".ToLower();
}