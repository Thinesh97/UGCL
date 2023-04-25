
        function CustomAlert() {
            this.render = function (dialog) {
                var winW = window.innerWidth;
                var winH = window.innerHeight;
                var dialogoverlay = document.getElementById('dialogoverlay');
                var dialogbox = document.getElementById('dialogbox');
                dialogoverlay.style.display = "block";
                dialogoverlay.style.height = winH + "px";
                // dialogbox.style.left = "40%";
                dialogbox.style.bottom = "5px";
                dialogbox.style.display = "block";
                document.getElementById('dialogboxbody').innerHTML = dialog;
                document.getElementById('dialogboxfoot').innerHTML = '<button class="btn btn-warning" onclick="Alert.ok()">' + 'X</button>';

            }
            this.ok = function () {
                document.getElementById('dialogbox').style.display = "none";
                document.getElementById('dialogoverlay').style.display = "none";
            }
        }
var Alert = new CustomAlert();



function CustomAlert1() {
    this.render = function (dialog) {
        var winW1 = window.innerWidth;
        var winH1 = window.innerHeight;
        var dialogoverlay1 = document.getElementById('dialogoverlay1');
        var dialogbox1 = document.getElementById('dialogbox1');
        dialogoverlay1.style.display = "block";
        dialogoverlay1.style.height = winH1 + "px";
        // dialogbox1.style.left = "40%";
        dialogbox1.style.bottom = "5px";
        dialogbox1.style.display = "block";
        document.getElementById('dialogboxbody1').innerHTML = dialog;
        document.getElementById('dialogboxfoot1').innerHTML = '<button class="btn btn-warning" onclick="Alert1.ok()">' + 'X</button>';
    }
    this.ok = function () {
        document.getElementById('dialogbox1').style.display = "none";
        document.getElementById('dialogoverlay1').style.display = "none";
    }
}
var Alert1 = new CustomAlert1();





function CustomAlert2() {
    this.render = function (dialog) {
        var winW2 = window.innerWidth;
        var winH2 = window.innerHeight;
        var dialogoverlay2 = document.getElementById('dialogoverlay2');
        var dialogbox2 = document.getElementById('dialogbox2');
        dialogoverlay2.style.display = "block";
        dialogoverlay2.style.height = winH2 + "px";
        //dialogbox2.style.left = "40%";
        dialogbox2.style.bottom = "5px";
        dialogbox2.style.display = "block";
        document.getElementById('dialogboxbody2').innerHTML = dialog;
        document.getElementById('dialogboxfoot2').innerHTML = '<button class="btn btn-warning" onclick="Alert2.ok()">' + 'X</button>' + '<button class="btn btn-default" onclick="Alert2.ok()">' + 'Cancel</button>';
    }
    this.ok = function () {
        document.getElementById('dialogbox2').style.display = "none";
        document.getElementById('dialogoverlay2').style.display = "none";
    }
}

var Alert2 = new CustomAlert2();





