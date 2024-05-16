import * as React from "react";
import * as ReactDOM from "react-dom";
import Application from "./app/App";

import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import 'dayjs/locale/pt';

ReactDOM.render(
<LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="pt">
    <Application />
</LocalizationProvider>, 
document.getElementById("root"));
