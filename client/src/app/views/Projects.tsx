import * as React from 'react';
import { Table }  from "../components/Table";
import { CreateProjectModal } from "../components/CreateProjectModal";
import { getAll } from "../api/projects";
import { Project } from "../types/project";

export default function Projects() {
    const [open, setOpen] = React.useState(false);
    const [projects, setProjects] = React.useState<Project[]>([]);
    const [loading, setLoading] = React.useState(false);
    const [searchTerm, setSearchTerm] = React.useState("");
    const [sort, setSorting] = React.useState("deadline");

    const handleSortingChange = () => {
        const order = sort === "deadline" ? "deadline_desc" : "deadline";
        setSorting(order);
        refreshTable(searchTerm, order);
       };

    const handleSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTerm(event.target.value)
        refreshTable(event.target.value, sort);
    }

    React.useEffect(() => {
        refreshTable();
      }, []);

      const refreshTable = (searchTerm: String = "", sortOrder: String = "deadline") => {
        setLoading(true)
        getAll(searchTerm,sortOrder)
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
                        <input
                            className="border rounded-full py-2 px-4"
                            type="search"
                            placeholder="Search"
                            aria-label="Search"
                            onChange={handleSearch}
                        />
                </div>
            </div>

            <Table loading={loading} projects={projects} sorting={handleSortingChange}/>
        </>
    );
}
