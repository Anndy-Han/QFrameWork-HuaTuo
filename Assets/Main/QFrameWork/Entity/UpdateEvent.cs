using System;

namespace QFrameWork
{
    public class UpdateEvent : ITicker
    {
        Action m_updateEvent;
        private bool m_running;

        public UpdateEvent(Action updateEvent)
        {
            m_updateEvent = updateEvent;
        }

        public void SetUpdater(Action updater)
        {
            m_updateEvent = updater;
        }

        public void Pause()
        {
            m_running = false;
        }

        public void Resume()
        {
            m_running = true;
        }

        public void Start()
        {
            m_running = true;
            App.AddTicker(this);
        }

        public void Stop()
        {
            m_running = false;
        }

        public bool OnUpdate(float deltatime)
        {
            if (!m_running)
                return true;
            if (m_updateEvent != null)
            {
                m_updateEvent();
            }
            return false;
        }
    }
}
