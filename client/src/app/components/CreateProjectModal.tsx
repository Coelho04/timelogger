import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import { DateField } from '@mui/x-date-pickers/DateField';
import { create } from "../api/projects";

export const CreateProjectModal = ({...props}) => {
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
            const formJson = Object.fromEntries((formData as any).entries());
            const name = formJson.name;
            const deadline = formJson.deadline;

            create(name,deadline)
                .then(() => {
                    props.refreshTable();
                })
                .catch((err) => alert(err + 'An error has occurred while creating the project.'))
                .finally(() => handleClose());

            console.log(name);
            console.log(deadline);
          },
        }}
      >
        <DialogTitle>Create Project</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            required
            id="name"
            name="name"
            label="Project Name"
            type="text"
            variant="outlined"
            style={{margin:10}}
          />
          <br/>
          <DateField
          format='YYYY-MM-DD'
          required
          name="deadline"
          label="Project Deadline"
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