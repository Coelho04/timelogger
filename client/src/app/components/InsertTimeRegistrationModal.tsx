import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { create } from "../api/timeRegistration";

export const InserTimeRegistrationModal = ({...props}) => {
    const handleClose = () => {
      props.closeDialog(false);
    };

  return (
    <React.Fragment>
      <Dialog
        open={ props.modalState }
        onClose={ handleClose }
        PaperProps={{
          component: 'form',
          onSubmit: (event: React.FormEvent<HTMLFormElement>) => {
            event.preventDefault();
            const formData = new FormData(event.currentTarget);
            debugger;
            const formJson = Object.fromEntries((formData as any).entries());
            const projectId = props.projectDetails.projectId;
            const duration = formJson.duration;
            const description = formJson.description;

            create(projectId,duration,description)
                .catch((err) => alert(err + 'An error has occurred while creating the project.'))
                .finally(() => handleClose());
          },
        }}
      >
        <DialogTitle>Insert Time</DialogTitle>
        <DialogContent>

          <TextField
            id="projectName"
            name="name"
            label="Project Name"
            type="text"
            variant="outlined"
            disabled
            style={{margin:10}}
            value={props.projectDetails.projectName}
          />
          <br/>
        <TextField
            style={{margin:10}}
            id="duration"
            name="duration"
            label="Duration"
            type="number"
            variant="outlined"
            inputProps={{
              step: 0.5,
            }}
          />
          <br/>
          <TextField
            id="description"
            name="description"
            label="Description"
            type="text"
            variant="outlined"
            style={{margin:10}}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button type="submit">Save</Button>
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
};