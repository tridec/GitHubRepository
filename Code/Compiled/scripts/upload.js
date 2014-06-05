        function validateRUL(source, arguments) {
            try {
                //alert("TEST!!!!!");
                var radUpload = getRadUpload("rulUpload");  
                arguments.IsValid = radUpload.validateExtensions();

            }
            catch (e) {
                //catch and just suppress error
                //alert(e.description);
                arguments.IsValid = true;
            } 
        }
        
        function checkRequiredUpload(source, arguments)  
            {
                try{ 
                    var radUpload = getRadUpload("rulUpload");
                    var fileInputs = radUpload.getFileInputs();
                    if (fileInputs[0].value==0) {
                        arguments.IsValid = false;
                    } else {
                        arguments.IsValid = true;
                    }
                  }
                catch(e){
                    //catch and just suppress error
                    arguments.IsValid = true;
                }       
           }
        function overlay()
         { 
            //alert("This is it!");
            if (Page_ClientValidate()) 
            { 
               //alert("This is a test");
               el = document.getElementById("overlay"); 
               el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible"; 
               SubmitFocus();
            } 
            
         }
         function SubmitFocus()
         {
            if(document.getElementById("overlay").visible == true)
            {         
                document.getElementById("overlay").focus();
            }
         }