﻿using System.Collections.Generic;

namespace Data.Models
{

    public class BotViewModel
    {
        public class QrPayLoad
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public class GetStarted
        {
            public string payload { get; set; }
        }

    
        public class BotRequest
        {
            public string @object { get; set; }
            public List<BotEntry> entry { get; set; }
        }

        public class BotEntry
        {
            public string id { get; set; }
            public long time { get; set; }
            public List<BotMessageReceivedRequest> messaging { get; set; }
        }

        public class BotMessageReceivedRequest
        {
            public BotUser sender { get; set; }
            public BotUser recipient { get; set; }
            public string timestamp { get; set; }
            public BotMessage message { get; set; }
            public BotPostback postback { get; set; }

        }
        public class DefaultAction
        {
            public string type { get; set; }
            public string url { get; set; }
            public bool messenger_extensions { get; set; }
            public string webview_height_ratio { get; set; }
        }

        public class BotPostback
        {
            public string payload { get; set; }
        }
        public class BotMessageResponseForVideo
        {
            public BotUser recipient { get; set; }
            public MessageResponseForAttachment message { get; set; }

        }

        public class BotMessageResponse
        {
            public BotUser recipient { get; set; }
            public MessageResponse message { get; set; }

        }
        public class BotMessageResponseForGenericTemplate
        {
            public BotUser recipient { get; set; }
            public MessageResponseForGenericTemplate message { get; set; }

        }
        public class ShowTyping
        {
            public BotUser recipient { get; set; }
            public string sender_action { get; set; }
        }
        public class BotMessage
        {
            public string mid { get; set; }
            public List<MessageAttachment> attachments { get; set; }
            public long seq { get; set; }
            public string text { get; set; }
            public QuickReply quick_reply { get; set; }
        }

        public class BotUser
        {
            public string id { get; set; }
        }
        public class MessageResponseForAttachment
        {
            public MessageAttachmentForAttachment attachment { get; set; }
        }
        public class MessageResponse
        {
            public MessageAttachment attachment { get; set; }
            public List<QuickReply> quick_replies { get; set; }
            public string text { get; set; }
        }
        public class MessageResponseForGenericTemplate
        {
            public MessageAttachmentForGenericTemplate attachment { get; set; }
            public List<QuickReply> quick_replies { get; set; }
            public string text { get; set; }

        }

        public class QuickReply
        {
            
            public string content_type { get; set; }
            public string title { get; set; }
            public string payload { get; set; }
            public string image_url { get; set; }
            
        }

        public class ResponseButton
        {
            public string type { get; set; }
            public string title { get; set; }
            public string payload { get; set; }
            public string url { get; set; }
            public bool messenger_extensions { get; set; }
            public string webview_height_ratio { get; set; }
        }
        public class ResponseButtonForGenericTemplate
        {
            public string type { get; set; }
            public string title { get; set; }
            public string payload { get; set; }
            public string url { get; set; }
            public string webview_height_ratio { get; set; }
        }
        public class MessageAttachmentForAttachment
        {
            public string type { get; set; }
            public MessageAttachmentPayLoadForAttachment payload { get; set; }
        }
        public class MessageAttachment
        {
            public string type { get; set; }
            public MessageAttachmentPayLoad payload { get; set; }
        }
        public class MessageAttachmentForGenericTemplate
        {
            public string type { get; set; }
            public MessageAttachmentPayLoadForGenericTemplate payload { get; set; }
        }
        public class Button
        {
            public string title { get; set; }
            public string type { get; set; }
            public string payload { get; set; }
        }

        public class MessageAttachmentPayLoadForAttachment
        {
            public string url { get; set; }
          
        }
        public class MessageAttachmentPayLoad
        {
            public string template_type { get; set; }
            public List<Button> buttons  { get; set; }
            //public string url { get; set; }
            //add for list
            public string top_element_style { get; set; }
            public List<PayloadElements> elements { get; set; }
        }
        public class MessageAttachmentPayLoadForGenericTemplate
        {
            public string template_type { get; set; }
            public List<PayloadElementsForGenericTemplate> elements { get; set; }
        }

        public class PayloadElements
        {
            public string title { get; set; }
            public string image_url { get; set; }
            public string subtitle { get; set; }
            public List<ResponseButton> buttons { get; set; }
            public string item_url { get; set; }
            public DefaultAction default_action { get; set; }
        }
        public class PayloadElementsForGenericTemplate
        {
            public string title { get; set; }
            public string image_url { get; set; }
            public string subtitle { get; set; }
            public List<ResponseButtonForGenericTemplate> buttons { get; set; }
            public DefaultAction default_action { get; set; }
        }

        //public class MessageAttachmentPayLoad
        //{
        //    public string url { get; set; }
        //    public string template_type { get; set; }
        //    public string top_element_style { get; set; }
        //    public List<PayloadElements> elements { get; set; }
        //    public List<ResponseButtons> buttons { get; set; }
        //    public string recipient_name { get; set; }
        //    public string order_number { get; set; }
        //    public string currency { get; set; }
        //    public string payment_method { get; set; }
        //    public string order_url { get; set; }
        //    public string timestamp { get; set; }
        //    public Address address { get; set; }
        //    public Summary summary { get; set; }
        //}

        //public class PayloadElements
        //{
        //    public string title { get; set; }
        //    public string image_url { get; set; }
        //    public string subtitle { get; set; }
        //    public List<ResponseButtons> buttons { get; set; }
        //    public string item_url { get; set; }
        //    public int? quantity { get; set; }
        //    public decimal? price { get; set; }
        //    public string currency { get; set; }
        //}

        public class Address
        {
            
            public string street_1 { get; set; }
            public string city { get; set; }
            public string postal_code { get; set; }
            public string country { get; set; }
            public string state { get; set; }
        }
        public class Summary
        {
            public decimal? subtotal { get; set; }
            public decimal? shipping_cost { get; set; }
            public decimal? total_tax { get; set; }
            public decimal total_cost { get; set; }
        }
    }
}