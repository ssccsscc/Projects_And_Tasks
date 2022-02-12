using Logic.Model;
using System;
using static Logic.Model.ProjectModel;

namespace Representation.Model
{
    //'Copy' of the ProjectModel class without id used by a user to create a new Project
    //Other classes normally could be copied here as well but idk at this point I dont see need to do it
    /// <summary>
    /// Project infromation without Id (see ProjectModel)
    /// </summary>
    public class ProjectModel_Create : ProjectModel
    {
        private new int id { get; set; }
    }
}
