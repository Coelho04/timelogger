import * as React from 'react';
import { TimeRegistration } from "../types/timeregistration";
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";


export const TimeRegistrationTable = ({...props}) =>  {

    const handleClose = () => {
      props.closeDialog(false);
    };

    return (
      <React.Fragment>
        <Dialog
          open={ props.modalState }
          onClose={ handleClose }
        >
          <DialogTitle>Registered Times for {props.projectDetails.projectName}</DialogTitle>
          <DialogContent>
          <div className="projects_table">
       
        <table className="table-fixed w-full">
            <thead className="bg-gray-200">
                <tr>
                    <th className="border px-4 py-2 w-12">Id</th>
                    <th className="border px-4 py-2">Duration</th>
                    <th className="border px-4 py-2">Description</th>
                    <th className="border px-4 py-2 w-16"></th>
                </tr>
            </thead>
            <tbody>
            {props.timeRegistrations.map((timeRegistration: TimeRegistration) => (
              <tr>
                <td className="border px-4 py-2 w-12">{timeRegistration.id}</td>
                <td className="border px-4 py-2">{timeRegistration.duration}</td>
                <td className="border px-4 py-2">{timeRegistration.description}</td>
                <td className="border px-4 py-2">
                    <Tooltip title="Add Time">
                        <IconButton>
                        <DeleteIcon onClick={() => alert("success")}/>
                        </IconButton>
                    </Tooltip>
      
                </td>
              </tr>
            ))}
            </tbody>
        </table>
    </div>
            
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose}>Cancel</Button>
          </DialogActions>
        </Dialog>
      </React.Fragment>
    );
}
