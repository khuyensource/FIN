function CYBERAKT_Rotator()
{
    this.GlobalID='';
    this.ElementID='';
    this.ContainerID='';
    this.AutoStart=true;
    this.HideEffect=null;
    this.HideEffectDuration=0;
    this.Loop=true;
    this.PauseOnMouseOver=true;
    this.RotationType='ContentScroll';
    this.ScrollDirection='up';
    this.ScrollInterval=10;
    this.qw_bg=1;
    this.ShowEffect=null;
    this.ShowEffectDuration=0;
    this.SlidePause=2000;
    this.SmoothScrollSpeed='Medium';
    this.Slides=new Array();
    this.Tickers=new Array();
    this.LeadTickers=new Array();
    this.qw_e=-1;
    this.qw_m=0;
    this.qw_i=0;
    this.qw_f=0;
    this.qw_ae=0;
    this.qw_o=true;
    this.HasTickers=false;
    this.qw_cn=null;
    this.qw_u=false;
    this.qw_ba=false;
    this.qw_aj='';
};

function rcr_Start(qw_a)
{
    if(qw_a.RotationType=='SlideShow')
    {
        ss_ShowNextSlide(qw_a);
    }
    else
    {
        qw_bw(qw_a);
        scroll_ShowNextSlide(qw_a);
    };
};

function qw_bs(qw_a)
{
    if(!qw_a.qw_u)return null;
        qw_a.qw_u=false;
    if(!qw_a.qw_ba)
     {
        if(qw_a.RotationType=='SlideShow')
        {
           qw_cm(qw_a);
        }
        else
        {
           qw_bv(qw_a);
        };
     };
};

function qw_av(qw_a)
{
    if(qw_a.qw_u)return null;
    qw_a.qw_u=true;
    window.clearTimeout(qw_a.qw_f);
    window.clearTimeout(qw_a.qw_ae);
    if(qw_a.SlidePause==0)window.clearInterval(qw_a.qw_i);
    if(qw_a.RotationType=='SlideShow')
    {
        var qw_c=document.getElementById(qw_a.ContainerID);
        qw_c.style.visibility='visible';
    };
};

function qw_v(qw_a)
{
    if(qw_a.qw_e==-1)qw_a.qw_e=0;
    else if(qw_a.qw_e==qw_a.Slides.length-1)
        {
            qw_a.qw_e=0;qw_a.qw_o=false;
        }
    else qw_a.qw_e++;
};

function qw_bw(qw_a){var qw_c=document.getElementById(qw_a.ContainerID),qw_as=document.getElementById(qw_a.ElementID),qw_bf,qw_ay;switch(qw_a.ScrollDirection){case'up':qw_bf=parseInt(qw_as.style.height.replace('px',''))+'px';qw_ay='0px';break;case'left':qw_bf='0px';qw_ay=parseInt(qw_as.style.width.replace('px',''))+'px';break;};qw_c.style.top=qw_bf;qw_c.style.left=qw_ay;qw_c.style.visibility='visible';};function qw_bv(qw_a){if(qw_a.qw_i==0){scroll_ShowNextSlide(qw_a);}else if(qw_a.SlidePause==0){var qw_b='scroll_NextSlideToView('+qw_a.GlobalID+')';qw_a.qw_i=window.setInterval(qw_b,qw_a.ScrollInterval);};};function scroll_ShowNextSlide(qw_a){qw_v(qw_a);if(!qw_a.Loop&&!qw_a.qw_o){qw_av(qw_a);return null;};var qw_b='scroll_NextSlideToView('+qw_a.GlobalID+')';qw_a.qw_i=window.setInterval(qw_b,qw_a.ScrollInterval);};function scroll_NextSlideToView(qw_a){var qw_c=document.getElementById(qw_a.ContainerID),qw_ak=document.getElementById(qw_a.Slides[qw_a.qw_e]),qw_ax=parseInt(qw_c.style.top.replace('px','')),qw_ap=parseInt(qw_c.style.left.replace('px','')),qw_w=0,qw_s=0,qw_ad=document.getElementById(qw_a.Slides[qw_q(qw_a)]);if(!(qw_a.qw_o&&qw_a.qw_e==0)){qw_w=qw_ad.offsetHeight;qw_s=qw_ad.offsetWidth;};var qw_az=0;switch(qw_a.ScrollDirection){case'up':if(qw_a.RotationType=='ContentScroll'){qw_ax-=qw_a.qw_bg;}else{qw_az=abs(qw_w+qw_ax)/qw_p(qw_a);if(qw_az<=2)qw_az=1;qw_ax-=qw_az;};break;case'left':if(qw_a.RotationType=='ContentScroll'){qw_ap-=qw_a.qw_bg;}else{qw_az=abs(qw_s+qw_ap)/qw_p(qw_a);if(qw_az<=2)qw_az=1;qw_ap-=qw_az;};break;};qw_c.style.top=qw_ax+'px';qw_c.style.left=qw_ap+'px';if((qw_ax+qw_w==0&&qw_a.ScrollDirection=='up')||(qw_ap+qw_s==0&&qw_a.ScrollDirection=='left')){window.clearInterval(qw_a.qw_i);qw_a.qw_i=0;if(!(qw_a.qw_o&&qw_a.qw_e==0))qw_al(qw_a);if(qw_a.HasTickers){rcr_StartTickerSequence(qw_a);}else{var qw_b='scroll_ShowNextSlide('+qw_a.GlobalID+')';if(!qw_a.qw_u)qw_a.qw_f=window.setTimeout(qw_b,qw_a.SlidePause);};};};function qw_al(qw_a){var qw_c=document.getElementById(qw_a.ContainerID);if(qw_a.ScrollDirection=='up'){var qw_ad=document.getElementById(qw_a.Slides[qw_q(qw_a)]),qw_br=qw_ad.cloneNode(true);qw_c.removeChild(qw_ad);qw_c.style.top='0px';qw_c.appendChild(qw_br);qw_t(qw_a);}else{var qw_ck=document.getElementById(qw_a.ContainerRowID),qw_bq=qw_ck.cells[0],qw_cv=qw_ck.removeChild(qw_bq);qw_c.style.left='0px';var qw_cw=qw_ck.appendChild(qw_cv);qw_t(qw_a);};};function qw_q(qw_a){if(qw_a.qw_e==0)return qw_a.Slides.length-1;else return qw_a.qw_e-1;};function qw_p(qw_a){switch(qw_a.SmoothScrollSpeed){case'Slow':return 8;break;case'Medium':return 6;break;case'Fast':return 4;break;};};function qw_cm(qw_a){if(qw_a.HasTickers&&qw_a.qw_aj=='PlayingShowEffect')return null;if(!qw_a.qw_ba){ss_PlayHideEffect(qw_a);var qw_bb=0;if(qw_a.HideEffect)qw_bb=qw_a.HideEffectDuration;qw_b='ss_ShowNextSlide('+qw_a.GlobalID+')';qw_a.qw_f=window.setTimeout(qw_b,qw_bb);};};function ss_ShowNextSlide(qw_a){if(qw_a.qw_u)return null;qw_v(qw_a);var qw_c=document.getElementById(qw_a.ContainerID),qw_ak=document.getElementById(qw_a.Slides[qw_a.qw_e]);qw_c.innerHTML=qw_ak.innerHTML;qw_ak.innerHTML='';qw_t(qw_a);qw_bd(qw_a);if(qw_a.HasTickers){var qw_b='rcr_StartTickerSequence('+qw_a.GlobalID+')',qw_cr=window.setTimeout(qw_b,qw_a.ShowEffectDuration);}else{var qw_b='ss_DisplaySlide('+qw_a.GlobalID+')';qw_a.qw_f=window.setTimeout(qw_b,qw_a.ShowEffectDuration);};};function ss_DisplaySlide(qw_a){if(qw_a.qw_u)return null;qw_a.qw_aj='DisplayingSlide';window.clearTimeout(qw_a.qw_ae);window.clearTimeout(qw_a.qw_f);if(!qw_a.Loop&&qw_a.qw_e==qw_a.Slides.length-1){qw_av(qw_a);return null;};var qw_b='ss_PlayHideEffect('+qw_a.GlobalID+')';qw_a.qw_ae=window.setTimeout(qw_b,qw_a.SlidePause);var qw_bb=0;if(qw_a.HideEffect)qw_bb+=qw_a.HideEffectDuration;qw_bb+=qw_a.SlidePause;qw_b='ss_ShowNextSlide('+qw_a.GlobalID+')';qw_a.qw_f=window.setTimeout(qw_b,qw_bb);};function qw_bd(qw_a){qw_a.qw_aj='PlayingShowEffect';var qw_c=document.getElementById(qw_a.ContainerID);if(qw_c.filters&&qw_a.ShowEffect){qw_c.style.filter=qw_a.ShowEffect;qw_c.filters[0].apply();};qw_c.style.visibility='visible';if(qw_c.filters&&qw_a.ShowEffect)qw_c.filters[0].play();};function ss_PlayHideEffect(qw_a){qw_a.qw_aj='PlayingHideEffect';var qw_c=document.getElementById(qw_a.ContainerID);if(qw_c.filters&&qw_a.HideEffect){qw_c.style.filter=qw_a.HideEffect;qw_c.filters[0].apply();};var qw_ak=document.getElementById(qw_a.Slides[qw_a.qw_e]);qw_ak.innerHTML=qw_c.innerHTML;qw_c.style.visibility='hidden';if(qw_c.filters&&qw_a.HideEffect)qw_c.filters[0].play();};function rcr_StartTickerSequence(qw_a){qw_a.qw_aj='RunningTickers';qw_a.qw_ba=true;rcr_StartTicker(qw_a.LeadTickers[qw_a.qw_m]);};function rcr_EndTickerSequence(qw_a){qw_a.qw_ba=false;if(!qw_a.qw_u){if(qw_a.RotationType=='SlideShow'){ss_DisplaySlide(qw_a);}else{var qw_b='scroll_ShowNextSlide('+qw_a.GlobalID+')';qw_a.qw_f=window.setTimeout(qw_b,qw_a.SlidePause);};};qw_aq(qw_a);};function qw_aq(qw_a){if(qw_a.qw_m==qw_a.LeadTickers.length-1)qw_a.qw_m=0;else qw_a.qw_m++;};function qw_t(qw_a){if(qw_a.HasTickers)for(var qw_ct=0;qw_ct<qw_a.Tickers.length;qw_ct++)qw_af(qw_a.Tickers[qw_ct],'');};function ie_MsOver(qw_bp,qw_g){if(!qw_bp.contains(event.fromElement)&&qw_g)qw_av(qw_g);};function ie_MsOut(qw_bp,qw_g){if(!qw_bp.contains(event.toElement)&&qw_g)qw_bs(qw_g);};function ns_MsOver(qw_bo,qw_au,qw_g){if(qw_ah(qw_au,qw_bo)&&qw_g)qw_av(qw_g);};function ns_MsOut(qw_bo,qw_au,qw_g){if(!qw_ah(qw_au,qw_bo)&&qw_g)qw_bs(qw_g);};function qw_ah(qw_by,qw_bo){if(qw_by!=null){var qw_bp=document.getElementById(qw_by),qw_bz=qw_ci(qw_bp)-1,qw_cc=qw_ch(qw_bp)-1,qw_cj=qw_bz+qw_bp.offsetWidth+1,qw_cb=qw_cc+qw_bp.offsetHeight+1;if((qw_bo.pageX>qw_bz)&&(qw_bo.pageX<qw_cj)&&(qw_bo.pageY>qw_cc)&&(qw_bo.pageY<qw_cb)){return true;}else{return false;};}else{return false;};};function qw_ci(qw_h){var x=0;do{if(qw_h.style.position=='absolute'){return x+qw_h.offsetLeft;}else{x+=qw_h.offsetLeft;if(qw_h.offsetParent)if(qw_h.offsetParent.tagName=='TABLE')if(parseInt(qw_h.offsetParent.border)>0){x+=1;};};}while((qw_h=qw_h.offsetParent));return x;};function qw_ch(qw_h){var y=0;do{if(qw_h.style.position=='absolute'){return y+qw_h.offsetTop;}else{y+=qw_h.offsetTop;if(qw_h.offsetParent)if(qw_h.offsetParent.tagName=='TABLE')if(parseInt(qw_h.offsetParent.border)>0){y+=1;};};}while((qw_h=qw_h.offsetParent));return y;};function abs(x){if(x<0)return-x;else return x;};
