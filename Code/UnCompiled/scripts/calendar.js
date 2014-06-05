// Accessible Date Picker Calendar - webSemantics
// http://www.websemantics.co.uk/tutorials/accessible_date_picker_calendar/

var baseURL = "index.html"
var dbSTR = "?strDate="
var today_date = new Date()
var months = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December')
var daysofweek = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday')

// get date from the url otherwise use the current date
var date = new Date()
x = window.location.href
x = x.replace(/%2F/g, "/")
if (x.slice(x.length - 19, x.length - 10) == dbSTR) date = new Date(x.slice(x.length - 4, x.length), x.slice(x.length - 7, x.length - 5) - 1, x.slice(x.length - 10, x.length - 8))


function full_date(d, m, y) {  // m = 1-12 not 0-11
    //***Bottom Date on Calendar***
    t_date = new Date(y, m - 1, d)
    return (daysofweek[t_date.getDay()] + ", " + t_date.getDate() + " " + months[t_date.getMonth()] + " " + t_date.getFullYear())
}


function addlead0(x) { return ((x < 10) ? "0" + x : x) }


function backayear(d, m, y, txtBox, cal_div, date_range_start, date_range_end, fieldText) {  // m = 1-12 not 0-11
    // + "', '" + date_range_start + "', '" + date_range_end + "');"

    y--;
    if ((m == 3) && (d > 28)) {
        d = 29
        t_date = new Date(y, 1, d)
        if (d != t_date.getDate()) d--
    }
    //  if (((m==12)||(m==10)||(m==7)||(m==5))&&(d==31)) d--
    //  if (m==1) y--
    //  m--
    calendar(m - 1, d, y, txtBox, cal_div, date_range_start, date_range_end, fieldText);
    //  return (addlead0(d)+"/"+addlead0((m==0)?12:m)+"/"+y)
    return
}


function backamonth(d, m, y, txtBox, cal_div, date_range_start, date_range_end, fieldText) {  // m = 1-12 not 0-11
    // + "', '" + date_range_start + "', '" + date_range_end + "');"
    if ((m == 3) && (d > 28)) {
        d = 29
        t_date = new Date(y, 1, d)
        if (d != t_date.getDate()) d--
    }
    if (((m == 12) || (m == 10) || (m == 7) || (m == 5)) && (d == 31)) d--
    if (m == 1) y--
    m--
    if (m == 0)
        m = 12
    calendar(m - 1, d, y, txtBox, cal_div, date_range_start, date_range_end, fieldText);
    //  return (addlead0(d)+"/"+addlead0((m==0)?12:m)+"/"+y)
    return
}


function forwardamonth(d, m, y, txtBox, cal_div, date_range_start, date_range_end, fieldText) {  // m = 1-12 not 0-11
    if ((m == 1) && (d > 28)) {
        d = 29
        t_date = new Date(y, 1, d)
        if (d != t_date.getDate()) d--
    }
    if (((m == 3) || (m == 5) || (m == 8) || (m == 10)) && (d == 31)) d--
    if (m == 12) y++
    m++
    if (m == 13)
        m = 1
    //  return (addlead0(d)+"/"+addlead0((m==13)?1:m)+"/"+y)
    calendar(m - 1, d, y, txtBox, cal_div, date_range_start, date_range_end, fieldText);

}


function forwardayear(d, m, y, txtBox, cal_div, date_range_start, date_range_end, fieldText) {  // m = 1-12 not 0-11
    y++;
    if ((m == 1) && (d > 28)) {
        d = 29
        t_date = new Date(y, 1, d)
        if (d != t_date.getDate()) d--
    }
    //  if (((m==3)||(m==5)||(m==8)||(m==10))&&(d==31)) d--
    //  if (m==12) y++
    //  m++
    //  return (addlead0(d)+"/"+addlead0((m==13)?1:m)+"/"+y)
    calendar(m - 1, d, y, txtBox, cal_div, date_range_start, date_range_end, fieldText);

}

function calendar(month, day, year, txtBox, cal_div, date_range_start, date_range_end, fieldText) {
    // modified version of calendar. Pulls the date from the database, sets its value to txtBox, and splits the value
    // and sets it as the starting point on the calendar.
    //*** Added to set default selected calendar date to the appropriate date if one has already been assigned.
    var newdate = document.getElementById(txtBox).value;

    if (newdate != "") {
        var newdatesplit = newdate.split("/", 3);
        var current_day = newdatesplit[1];
        var current_month = newdatesplit[0] - 1;
        var current_year = newdatesplit[2];
        calID = "calendar_" + cal_div;
    }
    else {
        current_day = date.getDate()
        current_month = date.getMonth()
        current_year = date.getFullYear()
        calID = "calendar_" + cal_div;
    }


    if (date_range_end == null)
        date_2 = -1;
    else if (date_range_end == 0)  // disable all dates greater than today
        date_2 = new Date((current_month + 1) + "/" + current_day + "/" + current_year);
    else
        date_2 = new Date(date_range_end);


    if (date_range_start == null)
        date_1 = -1;
    else if (date_range_start == 0)  // disable all days prior to today
        date_1 = new Date((current_month + 1) + "/" + current_day + "/" + year);
    else
        date_1 = new Date(date_range_start);

    if (month == null || day == null || year == null) {
        day = current_day;
        month = current_month;
        year = current_year;
    }
    this_month = new Date(year, month, 1)
    next_month = new Date(year, month + 1, 1)

    //Find out when this month starts and ends.
    first_week_day = this_month.getDay()
    days_in_this_month = Math.floor((next_month.getTime() - this_month.getTime()) / (1000 * 60 * 60 * 24))
    if (month == 2) days_in_this_month = 31



    // determine if a date range has been passed and disable dates outside that range
    // dates before date_range_start are disabled, dates after date_range_end are disabled
    calendar_html = '<table id="calendar" align="center" summary="Date Picker Calendar - Select date ">';
    calendar_html += '<thead>';

    if (fieldText != null) {
        calendar_html += '<tr><td colspan="7">' + '<b>' + fieldText + '</b>' + '</td></tr>'
    }

    // original code  calendar_html+='<tr><td><strong><a href="'+baseURL+dbSTR+backamonth(day,month+1,year)+'" title="Previous month: '+months[(month==0)?11:month-1]+'">&laquo;</a></strong></td><th colspan="5" class="heading">'+months[month]+' '+year+'</th><td><strong><a href="'+baseURL+dbSTR+forwardamonth(day,month+1,year)+'" title="Next month: '+months[(month==11)?0:month+1]+'">&raquo;</a></strong></td></tr>'
    var strBack_1_Year = "javascript:backayear(" + day + ", " + (parseInt(month) + 1) + ", " + year + ", '" + txtBox + "', '" + cal_div + "', '" + date_range_start + "', '" + date_range_end + "', '" + fieldText + "');return false;"
    var strFwd_1_Year = "javascript:forwardayear(" + day + ", " + (parseInt(month) + 1) + ", " + year + ", '" + txtBox + "', '" + cal_div + "', '" + date_range_start + "', '" + date_range_end + "', '" + fieldText + "');return false;"


    var strBack_1_Month = "javascript:backamonth(" + day + ", " + (parseInt(month) + 1) + ", " + year + ", '" + txtBox + "', '" + cal_div + "', '" + date_range_start + "', '" + date_range_end + "', '" + fieldText + "');return false;"
    var strFwd_1_Month = "javascript:forwardamonth(" + day + ", " + (parseInt(month) + 1) + ", " + year + ", '" + txtBox + "', '" + cal_div + "', '" + date_range_start + "', '" + date_range_end + "', '" + fieldText + "');return false;"

    //  calendar_html+='<tr><td><strong><a href="#" onclick="' + strBack_1_Month + '" title="Previous month: '+months[(month==0)?11:month-1]+'">&laquo;</a></strong></td><th colspan="5" class="heading">'+months[month]+' '+year+'</th><td><strong><a href="'+baseURL+dbSTR+forwardamonth(day,month+1,year)+'" title="Next month: '+months[(month==11)?0:month+1]+'">&raquo;</a></strong></td></tr>'
    // back a month, but no year....  calendar_html+='<tr><td><strong><a href="#" onclick="' + strBack_1_Month + '" title="Previous month: '+months[(month==0)?11:month-1]+'">&laquo;</a></strong></td><th colspan="5" class="heading">'+months[month]+' '+year+'</th><td><strong><a href="#" onclick="' + strFwd_1_Month + '" title="Next month: '+months[(month==11)?0:month+1]+'">&raquo;</a></strong></td></tr>'
    calendar_html += '<tr><td><strong><a id="' + calID + '" href="#" onclick="' + strBack_1_Year + '" title="Previous year: ' + (year - 1) + '">&laquo;</a></strong></td><td><strong><a href="#" onclick="' + strBack_1_Month + '" title="Previous month: ' + months[(month == 0) ? 11 : month - 1] + '">&lsaquo;</a></strong></td><th colspan="3" class="heading">' + months[month] + ' ' + year + '</th><td><strong><a href="#" onclick="' + strFwd_1_Month + '" title="Next month: ' + months[(month == 11) ? 0 : month + 1] + '">&rsaquo;</a></strong></td><td><strong><a href="#" onclick="' + strFwd_1_Year + '" title="Next year: ' + (year + 1) + '">&raquo;</a></strong></td></tr>'
    //  calendar_html+='<tr><td><strong><a href="#" onclick="' + strBack_1_Year + '" title="Previous year: '+ (year-1) +'">&laquo;</a></strong></td><td><strong><a href="#" onclick="' + strBack_1_Month + '" title="Previous month: '+months[(month==0)?11:month-1]+'">&lsaquo;</a></strong></td><th colspan="3" class="heading">'+months[month]+' '+year+'</th><td><strong><a href="#" onclick="' + strFwd_1_Month + '" title="Next month: '+months[(month==11)?0:month+1]+'">&rsaquo;</a></strong></td><td><strong><a href="#" onclick="' + strFwd_1_Year + '" title="Next year: '+ (year+1) +'">&raquo;</a></strong></td></tr>'

    //alert("debug, calendar_html= " + calendar_html);

    calendar_html += '<tr class="days"><th scope="col" class="weekend"><abbr title="Sunday">S</abbr></th><th scope="col"><abbr title="Monday">M</abbr></th><th scope="col"><abbr title="Tuesday">T</abbr></th><th scope="col"><abbr title="Wednesday">W</abbr></th><th scope="col"><abbr title="Thursday">T</abbr></th><th scope="col"><abbr title="Friday">F</abbr></th><th scope="col" class="weekend"><abbr title="Saturday">S</abbr></th></tr>'
    calendar_html += '</thead>'
    calendar_html += '<tbody>'
    calendar_html += '<tr>'

    for (week_day = 0; week_day < first_week_day; week_day++) { calendar_html += ((week_day == 0) || (week_day == 6)) ? '<td class="weekend">&nbsp;</td>' : '<td>&nbsp;</td>' }

    week_day = first_week_day
    mm = addlead0(next_month.getMonth())
    mm = (mm == 0) ? 12 : mm

    //  for(day_counter=1;day_counter<=days_in_this_month;day_counter++) {
    //    week_day%=7
    //    if(week_day==0) calendar_html+='</tr><tr>'
    //
    // uncomment if you want weekends to be disabled    if ((week_day==0)||(week_day==6))
    // uncomment if you want weekends to be disabled      calendar_html+='<td class="weekend">'+day_counter+'</td>'
    // uncomment if you want weekends to be disabled    else {
    //      calendar_html+='<td'
    //      if(day==day_counter) calendar_html+=' class="current"'
    // tcl local mod
    //     cal_date = mm + "/" + addlead0(day_counter) + "/" + year ;
    // original code      calendar_html+='><a title="'+full_date(day_counter,mm,year)+'" href="'+baseURL+dbSTR+ addlead0(day_counter)+"/"+mm+"/"+year+'">'+day_counter+'</a></td>'
    // tcl local mod
    //      calendar_html+='><a title="'+full_date(day_counter,mm,year) + '" href="#" onclick="javascript:saveDate(\'' + txtBox + '\', \'' + cal_date + '\', \'' + cal_div + '\');">' + day_counter + '</a></td>';
    // uncomment if you want weekends to be disable    }
    //    week_day++
    //  }


    for (day_counter = 1; day_counter <= days_in_this_month; day_counter++) {


        // check to see if a date_range was specified, if so, disable dates that fall outside the range
        // date_1 and date_2 were assigned a value of -1 if no start or end range was specified when this function was called....
        date_day_counter = new Date((month + 1) + "/" + day_counter + "/" + year);

        if ((date_1 > -1) && (date_day_counter < date_1))
            disabled = true;
        else if ((date_2 > -1) && (date_day_counter > date_2))
            disabled = true;
        else
            disabled = false;

        week_day %= 7
        if (week_day == 0) calendar_html += '</tr><tr>'

        if (disabled)
            calendar_html += '<td class="weekend">' + day_counter + '</td>';
        else {
            calendar_html += '<td'
            if (day == day_counter) calendar_html += ' class="current"'
            cal_date = mm + "/" + addlead0(day_counter) + "/" + year;
            calendar_html += '><a title="' + full_date(day_counter, mm, year) + '" href="#" onclick="javascript:saveDate(\'' + txtBox + '\', \'' + cal_date + '\', \'' + cal_div + '\');return false;">' + day_counter + '</a></td>';
        }
        week_day++
    }

    for (week_day = week_day; week_day < 7; week_day++) { calendar_html += ((week_day == 0) || (week_day == 6)) ? '<td class="weekend">&nbsp;</td>' : '<td>&nbsp;</td>' }

    calendar_html += '</tr>'
    calendar_html += '</tbody>'
    //tfoot code for todays date is broken. Commented out to avoid page errors.
    calendar_html += '<tfoot>'
    calendar_html += '<tr><td colspan="7" class="foot"><div title="Today" style="color:black">' + full_date(today_date.getDate(), today_date.getMonth() + 1, today_date.getFullYear()) + '</div></td></tr>'
    calendar_html += '</tfoot>'
    calendar_html += '</table>'

    //  document.write(calendar_html)  //Display the calendar.

    obj = document.getElementById(cal_div);

    if (obj) {
        obj.innerHTML = "<br />" + "<br />" + calendar_html;
        obj.style.display = "inline-block";
    }
    else
        document.write("<br/>***" + " not found....");

    obj2 = document.getElementById(calID);
    //  obj2=document.getElementById("zzcalID");
    if (obj2)
        obj2.focus();
    else
        alert("** calendar " + calID + " not found.");

}

function saveDate(where, what, cal_div) {
    var obj = document.getElementById(where);
    if (obj) {
        obj.value = what;
        var obj2 = document.getElementById(cal_div);
        if (obj2) obj2.style.display = "none";
        obj.focus();
    }
}
