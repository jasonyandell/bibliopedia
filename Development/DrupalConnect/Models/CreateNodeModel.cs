using System;

namespace DrupalConnect.Models
{
    public class CreateNodeModel
    {
        public CreateNodeModel(string title, string datePublished, string startPage="1", string endPage="1")
        {
            Title = title;
            DatePublished = datePublished;
            StartPage = startPage;
            EndPage = endPage;
            Creator = new string[]{};
            References = new string[]{};
            Citations = new string[]{};
        }

        public string LogMessage { get; set; }

        public string ContentCreatorName { get; set; }

        public string[] Citations { get; set; }

        public string SubjectHeading { get; set; }

        public string SelfCitation { get; set; }

        public string[] References { get; set; }

        public string DOI { get; set; }

        public string Issue { get; set; }

        public string Volume { get; set; }

        public string Journal { get; private set; }

        public string[] Creator { get; set; }

        public string EndPage { get; set; }

        public string StartPage { get; set; }

        public string DatePublished { get; set; }

        public string Title { get; set; }

        public string ToPostData()
        {
            return
                "title=ttitle&changed=&form_build_id=form-QAdhjFzTfET0EavLyjyESAk6y_qqI93v45HIwjRsyOk&form_token=LuwJH4m3XCryMkdzBWwcatatquIfWRunoRNeNthB22E&form_id=journal_article_node_form&field_creator%5Bund%5D%5B0%5D%5Bvalue%5D=auth0&field_creator%5Bund%5D%5B0%5D%5B_weight%5D=0&field_creator%5Bund%5D%5B1%5D%5Bvalue%5D=auth1&field_creator%5Bund%5D%5B1%5D%5B_weight%5D=1&field_journal%5Bund%5D%5B0%5D%5Btarget_id%5D=journal&field_volume%5Bund%5D%5B0%5D%5Bvalue%5D=0&field_issue%5Bund%5D%5B0%5D%5Bvalue%5D=0&field_issued%5Bund%5D%5B0%5D%5Bvalue%5D=datepub&field_startpage%5Bund%5D%5B0%5D%5Bvalue%5D=0&field_endpage%5Bund%5D%5B0%5D%5Bvalue%5D=0&field_doi%5Bund%5D%5B0%5D%5Bvalue%5D=doi&field_references%5Bund%5D%5B0%5D%5Bvalue%5D=&field_references%5Bund%5D%5B0%5D%5B_weight%5D=0&field_bibliographiccitation%5Bund%5D%5B0%5D%5Bvalue%5D=&field_subject%5Bund%5D=&field_cites%5Bund%5D%5B0%5D%5Btarget_id%5D=&field_cites%5Bund%5D%5B0%5D%5B_weight%5D=0&menu%5Blink_title%5D=&menu%5Bdescription%5D=&menu%5Bparent%5D=main-menu%3A0&menu%5Bweight%5D=0&book%5Bbid%5D=0&book%5Bplid%5D=-1&book%5Bweight%5D=0&path%5Bpathauto%5D=1&comment=2&name=jasony&date=&workbench_moderation_state_new=draft&log=Created+by+jasony.&additional_settings__active_tab=edit-menu&op=Save";
                "title=ttitle&changed=&form_build_id=form-jiConcJI0UxcWnkThFcWsCqiiwqM4-Bwo1z1EBRANxg&form_token=LuwJH4m3XCryMkdzBWwcatatquIfWRunoRNeNthB22E&form_id=journal_article_node_form&field_creator%5Bund%5D%5B0%5D%5Bvalue%5D=auth0&field_creator%5Bund%5D%5B0%5D%5B_weight%5D=0&field_journal%5Bund%5D%5B0%5D%5Btarget_id%5D=tjjournal&field_volume%5Bund%5D%5B0%5D%5Bvalue%5D=1&field_issue%5Bund%5D%5B0%5D%5Bvalue%5D=1&field_issued%5Bund%5D%5B0%5D%5Bvalue%5D=dpub&field_startpage%5Bund%5D%5B0%5D%5Bvalue%5D=1&field_endpage%5Bund%5D%5B0%5D%5Bvalue%5D=1&field_doi%5Bund%5D%5B0%5D%5Bvalue%5D=&field_references%5Bund%5D%5B0%5D%5Bvalue%5D=&field_references%5Bund%5D%5B0%5D%5B_weight%5D=0&field_bibliographiccitation%5Bund%5D%5B0%5D%5Bvalue%5D=&field_subject%5Bund%5D=&field_cites%5Bund%5D%5B0%5D%5Btarget_id%5D=&field_cites%5Bund%5D%5B0%5D%5B_weight%5D=0&menu%5Blink_title%5D=&menu%5Bdescription%5D=&menu%5Bparent%5D=main-menu%3A0&menu%5Bweight%5D=0&book%5Bbid%5D=0&book%5Bplid%5D=-1&book%5Bweight%5D=0&path%5Bpathauto%5D=1&comment=2&name=jasony&date=&workbench_moderation_state_new=draft&log=Created+by+jasony.&additional_settings__active_tab=edit-menu&op=Save"

            // This is not the way I'd like to do this going forward
            // It is just the way that was simplest and safest today
            // I see maintainability issues all over this method
            // Using ToString for this conflates Vieone view of the data with the Data
            var s =
                String.Format(
                    @"title={0}+&" + "changed=&" +
                // Trying to get away without this 
                //"form_build_id=form-lY2ZEfxR97HOJo2TFR_kaz88FPBt4k7nd2mBnBBLQ98&" +
                    "form_build_id=form-QAdhjFzTfET0EavLyjyESAk6y_qqI93v45HIwjRsyOkform_token=LuwJH4m3XCryMkdzBWwcatatquIfWRunoRNeNthB22E&" + "form_id=journal_article_node_form&",
                    Title);

            for (var i = 0; i < Creator.Length; i++)
            {
                s +=
                    String.Format(
                        "field_creator%5Bund%5D%5B{0}%5D%5Bvalue%5D={1}&" +
                        "field_creator%5Bund%5D%5B{0}%5D%5B_weight%5D=0&", i, Creator[i]);
            }

            s +=
                String.Format(
                    "field_journal%5Bund%5D%5B0%5D%5Btarget_id%5D={0}&" + "field_volume%5Bund%5D%5B0%5D%5Bvalue%5D={1}&" +
                    "field_issue%5Bund%5D%5B0%5D%5Bvalue%5D={2}&" + "field_issued%5Bund%5D%5B0%5D%5Bvalue%5D={3}&" +
                    "field_startpage%5Bund%5D%5B0%5D%5Bvalue%5D=12341234&" +
                    "field_endpage%5Bund%5D%5B0%5D%5Bvalue%5D=43214321&" + "field_doi%5Bund%5D%5B0%5D%5Bvalue%5D={4}&",
                    Journal, Volume, Issue, DatePublished, DOI);

            for (var i = 0; i < References.Length; i++)
            {
                s +=
                    String.Format(
                        "field_references%5Bund%5D%5B{0}%5D%5Bvalue%5D={1}&" +
                        "field_references%5Bund%5D%5B{0}%5D%5B_weight%5D=0&", i, References[i]);
            }

            s +=
                String.Format(
                    "field_bibliographiccitation%5Bund%5D%5B0%5D%5Bvalue%5D={0}&" + "field_subject%5Bund%5D={1}&",
                    SelfCitation, SubjectHeading);

            for (var i = 0; i < Citations.Length; i++)
            {
                s +=
                    String.Format(
                        "field_cites%5Bund%5D%5B{0}%5D%5Btarget_id%5D={1}&" +
                        "field_cites%5Bund%5D%5B{0}%5D%5B_weight%5D=0&", i, Citations[i]);
            }

            s +=
                String.Format(
                    "menu%5Blink_title%5D=&" + "menu%5Bdescription%5D=&" + "menu%5Bparent%5D=main-menu%3A0&" +
                    "menu%5Bweight%5D=0&" + "book%5Bbid%5D=new&" + "book%5Bplid%5D=-1&" + "book%5Bweight%5D=0&" +
                    "path%5Bpathauto%5D=1&" + "comment=2&" + "name={0}&" + "date=&" +
                    "workbench_moderation_state_new=draft&" + "log={1}&" + "additional_settings__active_tab=edit-book&" +
                    "op=Save", ContentCreatorName, LogMessage);

            return s;
        }


    }
}