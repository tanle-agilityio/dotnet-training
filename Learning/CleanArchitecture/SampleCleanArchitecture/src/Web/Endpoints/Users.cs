﻿using SampleCleanArchitecture.Infrastructure.Identity;

namespace SampleCleanArchitecture.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}
