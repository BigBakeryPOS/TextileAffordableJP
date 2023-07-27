////validate amount

//function validate() {
//    var pattern = /^-?[0-9]+(.[0-9]{1,2})?$/;
//    var text = document.getElementById('txtblnce').value;
//    if (text.match(pattern) == null) {
//        alert('the format is wrong');
//    }
//    else {
//        alert('OK');
//    }


//}



//pin code validation

//function validateZIP(field) {
//    var valid = "0123456789-";
//    var hyphencount = 0;

//    if (field.length != 5 && field.length != 7) {
//        alert("Please enter your 5 digit or 5 digit+4 zip code.");
//        return false;
//    }
//    for (var i = 0; i < field.length; i++) {
//        temp = "" + field.substring(i, i + 1);
//        if (temp == "-") hyphencount++;
//        if (valid.indexOf(temp) == "-1") {
//            alert("Invalid characters in your zip code.  Please try again.");
//            return false;
//        }
//        if ((hyphencount > 1) || ((field.length == 10) && "" + field.charAt(5) != "-")) {
//            alert("The hyphen character should be used with a properly formatted 5 digit+four zip code, like '12345-6789'.   Please try again.");
//            return false;
//        }
//    }
//    return true;
//}
//when blank field is not allowed
function blankchk(x, y) {
    if (x.value == "") {
        alert("Please enter a valid value for " + y);
        x.focus();
        return false;
    }
    return true;
}



//validation for phone numbers
function phonechk(x) {
    var num = /[0-9\-\s]+/
    numflag = x.value.match(num);
    if (numflag != x.value) {
        alert("Please enter valid phone number")
        x.focus();
        x.select();
        return false;
    }
    return true;
}
function nochk(x, y) {
    var num = /[0-9\-\s]+/
    numflag = x.value.match(num);
    if (numflag != x.value) {
        alert("Please enter valid " + y)
        x.focus();
        x.select();
        return false;
    }
    return true;
}

//Only numbers are allowed
function numchk(x) {
    var num = /[0-9]+/
    numflag = x.value.match(num);
    if (numflag != x.value) {
        alert("Please enter numbers only")
        x.focus();
        x.select();
        return false;
    }
    return true;
}

//Email validation
function emailchk(x) {
    var email = /[-a-zA-Z0-9_\.]+@[-a-zA-Z0-9]+\.[-a-zA-Z0-9\.]+/;
    var eflag = x.value.match(email);
    if (eflag != x.value) {
        alert("Please enter a valid email id")
        x.focus();
        x.select();
        return false;
    }
    return true;
}

//Only alphabets are allowed
function alphachk(x) {
    var alpha = /[a-zA-Z]+/
    alphaflag = x.value.match(alpha);
    if (alphaflag != x.value) {
        alert("Please enter alphabets only.")
        x.focus();
        x.select();
        return false;
    }
    return true;
}

//Validation for userid, where alphanumeric and under score is allowed 
function useridchk(x) {
    var usr = /[a-zA-Z0-9\_]+/;
    uidflag = x.value.match(usr);
    if (uidflag != x.value) {
        alert("Please enter valid user id.")
        x.focus();
        x.select();
        return false;
    }
    return true;
}

//First element of dropdown cannot be selected 
function dropdownchk(x, y) {
    if (x.selectedIndex == 0) {
        alert("Please select one option for " + y);
        //x.options[0].focus();
        return false;
    }
    return true;
}

//Validation when for confirm password
function confpswdchk(pass, confpass) {
    if (confpass.value != pass.value) {
        alert("The confirm password should be same as password");
        confpass.focus();
        confpass.select();
        return false;
    }
    return true;
}

//Validation where only Alphabets and space is allowed.
function alphaspchk(x) {
    var sp = /[a-zA-Z\s]+/;
    alpflag = x.value.match(sp);
    if (alpflag != x.value) {
        alert("nothing but A-Z, a-z & space")
        x.focus();
        x.select();
        return false;
    }
    return true;
}

//validation of the date field
function datechk(dd, mm, yyyy) {
    switch (chkdate(dd, mm, yyyy)) {
        case 1:
            alert("Invalid From Day");
            return false;
            break;
        case 2:
            alert("Invalid From Month");
            return false;
            break;
        case 3:
            alert("Invalid From Year");
            return false;
            break;
        case 4:
            alert("This month has only 30 days");
            return false;
            break;
        case 5:
            alert("This is a leap year. Feb has only 29 days");
            return false;
            break;
        case 6:
            alert("This is not a leap year. Feb has only 28 days");
            return false;
            break;
        case 7:
            alert("This year is a leap year. Feb has only 29 days");
            return false;
            break;
        case 8:
            alert("The \"date\" cannot be greater than today\'s date");
            return false;
            break;
    }
    return true;
}

function chkdate(dd, mm, yyyy) {
    var dt = new Date();
    invdate = mm + "/" + dd + "/" + yyyy;
    var invdate = new Date(invdate);
    sysdate = dt.getUTCMonth() + 1 + '/' + dt.getUTCDate() + '/' + dt.getUTCFullYear(); var sysdate = new Date(sysdate); if (dd == 0 || dd > 31) return 1; if (mm == 0 || mm > 12) return 2;
    if (yyyy == 0) return 3;
    if ((mm == 4 || mm == 6 || mm == 9 || mm == 11) && (dd > 30)) return 4;
    if (mm == 2 && dd > 29 && yyyy % 400 == 0) return 5;
    if (mm == 2 && dd > 28 && yyyy % 4 != 0) return 6;
    if (mm == 2 && dd > 29 && yyyy % 4 == 0 && yyyy % 400 != 0) return 7; if (sysdate < invdate) return 8;
}

function chkbox(chk) {
    var flag = false;
    var val = new Array();
    var a = 0;
    if (ie) {
        if (!isNaN(chk.length)) {
            for (i = 0; i < chk.length; i++) {
                if (chk[i].checked == true) {
                    flag = true;
                    val[a] = chk[i].value;
                    a++;
                }
            }
        } else {
            if (isNaN(chk.length)) {
                if (chk.checked == true) {
                    flag = true;
                    val[a] = chk.value;
                }
            }
        }

        if (flag == false) {
            alert("Please select your choice.");
            return false;
        }
        for (ctr = 0; ctr < val.length; ctr++) {
            alert(ctr + " = " + val[ctr]);
        }

    }
    if (ns) {
        if (chk.length > 0) {
            for (i = 0; i < chk.length; i++) {
                if (chk[i].checked == true) {
                    flag = true;
                    val[a] = chk[i];
                    a++;
                }
            }
        } else {
            if (chk.length <= 0) {
                if (chk.checked == true) {
                    flag = true;
                    val[a] = chk.value;
                }
            }
        }

        if (flag == false) {
            alert("Please select your choice.");
            return false;
        }
    }
}
