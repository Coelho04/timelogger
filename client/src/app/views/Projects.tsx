import * as React from 'react';
import { Table }  from "../components/Table";
import { CreateProjectModal } from "../components/CreateProjectModal";
import { getAll } from "../api/projects";
import { Project } from "../types/project";

export default function Projects() {
    const [open, setOpen] = React.useState(false);
    const [projects, setProjects] = React.useState<Project[]>([]);
    const [loading, setLoading] = React.useState(false);

    React.useEffect(() => {
        refreshTable();
      }, []);

      const refreshTable = () => {
        setLoading(true)
        getAll()
        .then(json => setProjects(json))
        .finally(() => {
          setLoading(false)
        })
      }

    return (
        <>
            <div className="flex items-center my-6">
                <div className="w-1/2">
                <button onClick={(e) => {e.preventDefault(); setOpen(true);}} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                        Add Project
                    </button>
                    <CreateProjectModal closeDialog={setOpen} modalState={open} refreshTable={refreshTable} />
                </div>

                <div className="w-1/2 flex justify-end">
                    <form>
                        <input
                            className="border rounded-full py-2 px-4"
                            type="search"
                            placeholder="Search"
                            aria-label="Search"
                        />
                        <button
                            className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
                            type="submit">
                            Search
                        </button>
                    </form>
                </div>
            </div>

            <Table loading={loading} projects={projects}/>
        </>
    );
}
