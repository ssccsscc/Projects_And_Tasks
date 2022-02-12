using Logic.Model;
using System;
using static Logic.Model.ProjectTaskModel;

namespace Representation.Model
{
    //'Copy' of the ProjectModel class without id used by a user to create a new Task
    //Other classes normally could be copied here as well but idk at this point I dont see need to do it
    /// <summary>
    /// Task infromation without Id (see ProjectTaskModel)
    /// </summary>
    public class ProjectTaskModel_Create : ProjectTaskModel
    {
        private new int id { get; set; } 
    }
}
