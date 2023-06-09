from kivy.app import App
from kivy.uix.boxlayout import BoxLayout
from kivy.factory import Factory
from kivy.core.window import Window

import urllib3, json, certifi

class JojoRobotRoot(BoxLayout):
    client = urllib3.PoolManager(cert_reqs='CERT_NONE')
    
    def __init__(self, **kwargs):
        super(JojoRobotRoot, self).__init__(**kwargs)

    def get_stuff(self):
        response = self.client.request('GET', 'https://10.7.0.1:4010/Home/Player')
        print(response.data)

    def post_stuff(self, chat):
        self.add_text("User: " + chat.text, 'green')
        data = f"message={chat.text}"
        response = self.client.request('POST', 'https://10.7.0.1:4010/Home/SendMessage', headers={'Content-Type':"application/x-www-form-urlencoded"}, body=data)
        
        message = json.loads(response.data)
        self.add_text("AI: " + message["choices"][0]['message']['content'], "red")
        chat.text = ""
        
    def add_text(self, text, color):
        obj = Factory.TextAdd(text=text)
        obj.red, obj.green, obj.blue = [1,0,0] if color == "red" else [0,1,0] if color == "green" else [0,0,1] if color == "blue" else [0,0,0]
        self.ids["grid_x"].add_widget(obj)
        self.ids["scroll_x"].size = (Window.width, Window.height)
	
class JojoRobotApp(App):
	def build(self):
		return JojoRobotRoot()

JojoRobotApp().run()
