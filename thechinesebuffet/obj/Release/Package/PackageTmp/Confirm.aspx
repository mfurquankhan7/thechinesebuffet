<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="thechinesebuffet.Confirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="https://test.sagepay.com/api/v1/js/sagepay-dropin.js"></script>
    <script>
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Payment successful   
    </div>
    </form>
</body>
    <script>    
        function PaymentSuccess(paRes, transid)
        {
            checkTransID(transid,paRes)
        alert("Hi");
        }
        function checkTransID(transID, Pares) {
            //curl https://test.sagepay.com/api/v1/transactions/<transactionId>/3d-secure \
            //-H "Authorization: Basic ZHE5dzZXa2tkRDJ5OGszdDRvbHF1OEg2YTB2dHQzSVk3VkVzR2hBdGFjYkNaMmI1VWQ6aG5vM0pURXdESHk3aEpja1U0V3V4ZmVUcmpEME45MnBJYWl0dVFCdzVNdGo3UkczVjh6T2RIQ1NQS3dKMDJ3QVY="  \
            //-H "Content-type: application/json" \
            //-X POST \
            //-d '{
            //   "paRes": "eJylV1tzqkgQfvdXpNxHKuEioKSIW8NFhAgCIipvBAcEERAQ0F+/aEzMyZ7d2rhUUdJtX76vp3sG2D+bXfxQwbwI0+Sliz9h3QeYeOk6TIKX7twaPQ66fw47rLXJIRRm0DvkcMiqsCjcAD6E65cugeEkSQ8wiuwxGNHHu0NWByYsLn9mbg6LZ+yJYaj+gKIGPYYc9Frb1uiac9imfMJZ9ENsY+fexk3KIet6e07WhiRDMBjGoleR3cFcFoY40YYjBgyLvsssenPUD+enogXahOuhkjN9Pa34hMCPgAqaZrQfh7uVECH1C4ueLdi1W8JhS4TCcYx+wLFnovdMtaEvejY7hwO79NDGxlsgX2W2LUjeFuw4HBA0i35KLGyyNIGtBcGin88seoOWucmV2vvVep9VrLUcsmW4+xVPD3/G+yx60bNF6ZaHYrhi0esT67lVNQQAcMA0qW0Nvl8tz4sJC71wiFEtovb34gXiIM3DcrM74/xVwaJnKOhlMdsSFc+zMEjafDl8aHsmKZ7XxUt3U5bZM4rWdf1U957SPECJlguKMWhrsy7C4I9u2z1XZ7iWEz+9x5t3kzQJPTcOT27ZtokKy026fvgE+7tQlnmOhqOmyD+24R49nEwezxqsh1Pdn6FAbyQuFfgv+b8HyQv3sdi4+H2pTejDc2vBh7kpv3R/GOQ9hpW7SeGn+a74X94/ow2TCsZpBtePxUf17qvAf8z/r8v+x6J149Pdrp3G4g4Y6C9VfMclhAEsyns64v5ueM9pu/Hhp8Po9Rm8MAwJGb9WmdPbYFlivOpYVu3Ilwu9L6GvhD9b7yrfRvnbVNyDp+O7h52iRINmsDDX1siQMKOitTEI7NqNS4mmhXDQZPN8V2Oq/4bFB0WndyLl9v0tR0oyPcKdCWT2vT7hhh3PEwtq76JYJUl7hIHgpGexjc1xS+IgiBtfCQ+yGlmDDNfM0HkVhR0lRpUyxQLSXc2qus6W64rAyfhEdHRGOyXC6qTjO7+MbWWFky+3CtwYv1fhFR7v3d2WFMYIbune68vDvAz9dnssf1x9VZZ5zeJ54NABqGUOAFk0wNxxIqBxwXa/2YYSU2McMOYjIHC+ahQ1b6wEu20hsVbs2UnUVYBJAJ+LHZ5TJUtiDk57fzEct4Yj2xKnKqgvhvxGHc3HSrUeB40YAYMLNJsDhcXb3PGtZ5KyiBud2RcH0Hw61OONp6mCUauWiKkWOGqRQSzOupNYa1Fr+aGLbmg7v4N7L9rO7+D+E9rAGdSCsVJeU0feVJ4GDJHjDCAEKwyosqR0QCpx4LWF5CWELmHOdmOJJE/QFh17M8xaT8jpSJ97ES26LiVoajAf487Si5YpclxNbEpBw71tb72gmBmdPC3oEMdzLZF6/gbRk+kibYpdYASUa3kkatERkjuQ95I3Z7wFr9VoChyDiHs4crAXadBMKL10ZW9JVJtO0NN525VTEAm9KY00SDiwZ40cg0Bt20T8Tmv0TksEUdIr+dm0dyD0SK9neZBtZ53j1BOker8Oj3VoaxliVttjeQC9fL4n7cSBdoiTxgFr0BMqIvONyHARHVlzaG6ILcNUg6PP7T1+cVjFA6cDRpJoWf4SQMetimmgFNMij5pYQxBfPtJJf3oKRAJoZb6hZ9LbQmiSUDGFKhuN2oa2QnqbyyfaVytavA70tyka3rTnufwc8Ls2uMvpPQP/x11N14f48ONTOxUM5+ivBm/mbjZSrWDylul7u5mYGlYF5Jwxj3pqZvlWxY6c4dvx+qjBqK8E5XKbIK+ZvTCPfKPwh/C0n3SWi4Lw3tbImEQ8yo2ko8vxQpxpp4jR9zpdxqRyXJdbaID9YE5yseAu/SVN2W4oiNUWwdNN47uIJHgDxe6kYcCrutfP+2N60tgTMZxfV+JK9Z23eH1x/iFxYADuEuzD/+Mgu63CVfN38Xqs/bLLXz4uLl8855fir19CfwGvWEqk"
            //}'
            var integrationKey = "dq9w6WkkdD2y8k3t4olqu8H6a0vtt3IY7VEsGhAtacbCZ2b5Ud";
            var integrationPassword = "hno3JTEwDHy7hJckU4WuxfeTrjD0N92pIaituQBw5Mtj7RG3V8zOdHCSPKwJ02wAV";
            authorize = integrationKey + ":" + integrationPassword;
            authorize = btoa(authorize);
            $.ajax({
                url: "https://test.sagepay.com/api/v1/transactions/"+transID+"/3d-secure",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", "Basic " + authorize);
                },
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                processData: false,
                data: '{"paRes": "' + Pares + '"}',
                success: function (result) {
                    
                    //sagepayCheckout();
                },
                error: function (e) {
                    alert("Cannot get data");
                }
            });
        }
</script>

</html>
