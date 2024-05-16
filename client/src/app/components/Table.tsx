import React from "react";
import { Project } from "../types/project";
import MoreTimeIcon from '@mui/icons-material/MoreTime';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import { InserTimeRegistrationModal } from "./InsertTimeRegistrationModal";
import { TimeRegistrationTable } from "./TimeRegistrationTable";
import { TimeRegistration } from "../types/timeregistration";
import { getAll } from "../api/timeRegistration";

export const Table = ({...props}) =>  {
    const [open, setOpen] = React.useState(false);
    const [openTimeRegistration, setTimeRegistrationOpen] = React.useState(false);

    const [projectDetails, setProjectDetails] = React.useState({
        projectId: "",
        projectName: ""
      })

      const [timeRegistrations, setTimeRegistrations] = React.useState<TimeRegistration[]>([]);

      function captureProject(clickedProject: Project) {
        let filtered = props.projects.filter((project: Project) => project.id === clickedProject.id)
        setProjectDetails({projectId: filtered[0].id, projectName: filtered[0].name})
      }

      function getTimeRegistrations(clickedProject: Project) {
        let filtered = props.projects.filter((project: Project) => project.id === clickedProject.id)
        setProjectDetails({projectId: filtered[0].id, projectName: filtered[0].name})

        getAll(filtered[0].id)
            .then(json => setTimeRegistrations(json))
            .finally(() => {
                setTimeRegistrationOpen(true)
            })
      }

    return (
        <div className="projects_table">
        {props.loading ? (
            <div>Loading...</div>
          ) : (
        <table className="table-fixed w-full">
            <thead className="bg-gray-200">
                <tr>
                    <th className="border px-4 py-2 w-12">Id</th>
                    <th className="border px-4 py-2">Project Name</th>
                    <th className="border px-4 py-2">Deadline</th>
                    <th className="border px-4 py-2">Completed</th>
                    <th className="border px-4 py-2 w-16"></th>
                </tr>
            </thead>
            <tbody>
            {props.projects.map((project: Project) => (
              <tr>
                <td className="border px-4 py-2 w-12" onClick={(e) => {e.preventDefault(); getTimeRegistrations(project);}}>
                    {project.id}
                    </td>
                <td className="border px-4 py-2">{project.name}</td>
                <td className="border px-4 py-2">{project.deadline}</td>
                <td className="border px-4 py-2">{project.isCompleted.toString()}</td>
                <td className="border px-4 py-2">
                    <Tooltip title="Add Time">
                        <IconButton>
                            <MoreTimeIcon onClick={(e) => {e.preventDefault(); captureProject(project); setOpen(true);}}/>
                        </IconButton>
                    </Tooltip>
                    <InserTimeRegistrationModal closeDialog={setOpen} modalState={open} projectDetails={projectDetails} />
                    <TimeRegistrationTable closeDialog={setTimeRegistrationOpen} modalState={openTimeRegistration} projectDetails={projectDetails} timeRegistrations={timeRegistrations} />
                </td>
              </tr>
            ))}
            </tbody>
        </table>
        )}
    </div>
    );
}
