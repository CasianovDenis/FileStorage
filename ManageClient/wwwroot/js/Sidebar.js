﻿document.addEventListener("DOMContentLoaded", function (event) {

    const showNavbar = (toggleId, navId, bodyId, headerId) => {
        const toggle = document.getElementById(toggleId),
            nav = document.getElementById(navId),
            bodypd = document.getElementById(bodyId),
            headerpd = document.getElementById(headerId)

        // Validate that all variables exist
        if (toggle && nav && bodypd && headerpd) {
            toggle.addEventListener('click', () => {
                // show navbar
                nav.classList.toggle('show')
                // change icon
                toggle.classList.toggle('bx-x')
                // add padding to body
                bodypd.classList.toggle('body-pd')
                // add padding to header
                headerpd.classList.toggle('body-pd')
            })
        }
    }

    showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header')

    /*===== LINK ACTIVE =====*/
    const linkColor = document.querySelectorAll('.nav_link')

    function colorLink() {
        if (linkColor) {
            linkColor.forEach(l => l.classList.remove('active'))
            this.classList.add('active')
        }
    }
    linkColor.forEach(l => l.addEventListener('click', colorLink))

    // Your code to run since DOM is loaded and ready
});

$(document).ready(function () {
    var click = 0;
    $('#header-toggle').click(function () {
        
        if (click == 0) {
            $('#user_icon').css("margin-left", "40%");
            $('#header-toggle').css("margin-left", "180px");
            click = 1;
        }
        else
        {
            $('#user_icon').css("margin-left", "0%");
            $('#header-toggle').css("margin-left", "10px");
            click = 0;
        }
    });

        
   
});